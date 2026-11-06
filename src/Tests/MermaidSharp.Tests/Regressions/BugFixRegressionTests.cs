using MermaidSharp.Configs;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MermaidSharp.Tests.Regressions
{
    [TestClass]
    public class BugFixRegressionTests
    {
        #region GitGraph

        [TestMethod]
        public void GitGraph_Commit_Throws_WhenIdAlreadyExists()
        {
            var graph = new GitGraph().Commit("c1");

            Assert.ThrowsExactly<InvalidOperationException>(() => graph.Commit("c1"));
        }

        [TestMethod]
        public void GitGraph_Merge_Throws_WhenIdAlreadyExists()
        {
            var graph = new GitGraph().Commit("c1").Branch("develop").Commit("c2").Checkout("main");

            Assert.ThrowsExactly<InvalidOperationException>(() => graph.Merge("develop", "c2"));
        }

        [TestMethod]
        public void GitGraph_Branch_Throws_WhenBranchAlreadyExists()
        {
            var graph = new GitGraph().Branch("develop");

            Assert.ThrowsExactly<InvalidOperationException>(() => graph.Branch("develop"));
            Assert.ThrowsExactly<InvalidOperationException>(() => graph.Branch("main"));
        }

        [TestMethod]
        public void GitGraph_Branch_SwitchesCurrentBranch()
        {
            var graph = new GitGraph().Commit("c1").Branch("develop");

            Assert.AreEqual("develop", graph.CurrentBranch);
        }

        [TestMethod]
        public void GitGraph_Merge_Throws_WhenMergingCurrentBranch()
        {
            var graph = new GitGraph().Commit("c1").Branch("develop").Commit("c2");

            Assert.ThrowsExactly<InvalidOperationException>(() => graph.Merge("develop"));
        }

        [TestMethod]
        public void GitGraph_CherryPick_Throws_WhenCommitDoesNotExist()
        {
            var graph = new GitGraph().Commit("c1").Branch("develop");

            Assert.ThrowsExactly<InvalidOperationException>(() => graph.CherryPick("unknown"));
        }

        [TestMethod]
        public void GitGraph_CherryPick_Throws_WhenCommitIsOnCurrentBranch()
        {
            var graph = new GitGraph().Commit("c1").Branch("develop").Commit("c2");

            Assert.ThrowsExactly<InvalidOperationException>(() => graph.CherryPick("c2"));
        }

        [TestMethod]
        public void GitGraph_CherryPickMerge_ResolvesParent_WhenBranchWasNotCheckedOutExplicitly()
        {
            var graph = new GitGraph()
                .Commit("c1")
                .Branch("develop")
                .Commit("c2")
                .Checkout("main")
                .Merge("develop", "m1")
                .Branch("hotfix")
                .CherryPick("m1");

            StringAssert.Contains(graph.CalculateDiagram(), "cherry-pick id:\"m1\" parent: \"c2\"");
        }

        #endregion

        #region Flowchart

        [TestMethod]
        public void Flowchart_LinkStyle_UsesIndexAmongAllLinks()
        {
            var flowchart = new FlowchartDiagram();
            flowchart.SubGraphs.Add(new FlowSubGraph("group",
                new List<FlowNode> { new FlowNode("a", "A"), new FlowNode("b", "B") },
                new List<FlowLink> { new FlowLink("a", "b") }));
            flowchart.Nodes.Add(new FlowNode("c", "C"));
            flowchart.Links.Add(new FlowLink("b", "c"));
            flowchart.Links.Add(new FlowLink("a", "c", linkstyle: "stroke:red"));

            var result = flowchart.CalculateDiagram();

            StringAssert.Contains(result, "linkStyle 2 stroke:red");
        }

        [TestMethod]
        public void FlowNode_Text_IsQuoted_WhenItContainsSpecialCharacters()
        {
            var node = new FlowNode("a", "Call f(x) \"now\"");

            Assert.AreEqual("a[\"Call f(x) #quot;now#quot;\"]", node.ToString());
        }

        [TestMethod]
        public void FlowNode_Text_IsNotQuoted_WhenPlain()
        {
            var node = new FlowNode("a", "Plain text");

            Assert.AreEqual("a[Plain text]", node.ToString());
        }

        [TestMethod]
        public void FlowNode_ClickAction_EscapesQuotes()
        {
            var node = new FlowNode("a", "A", clickAction: "https://example.com/?q=\"x\"");

            Assert.AreEqual("click a \"https://example.com/?q=%22x%22\"", node.ToClickString());
        }

        [TestMethod]
        public void FlowNode_Throws_WhenNameIsNull()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => new FlowNode(null, "A"));
            Assert.ThrowsExactly<ArgumentNullException>(() => new FlowLink(null, "b"));
            Assert.ThrowsExactly<ArgumentNullException>(() => new FlowSubGraph(null));
        }

        [TestMethod]
        public void FlowNode_NameSetter_RemovesSpaces()
        {
            var node = new FlowNode("a", "A");

            node.Name = "my node";

            Assert.AreEqual("mynode", node.Name);
        }

        #endregion

        #region Yaml / config

        [TestMethod]
        public void Title_IsQuoted_WhenItContainsColon()
        {
            var flowchart = new FlowchartDiagram("Step 1: init");
            flowchart.Nodes.Add(new FlowNode("a", "A"));

            StringAssert.Contains(flowchart.CalculateDiagram(), "title: \"Step 1: init\"");
        }

        [TestMethod]
        public void Config_StringValue_IsEscaped()
        {
            var graph = new GitGraph(config: new GitGraphConfig(mainBranchName: "a\"b"));

            StringAssert.Contains(graph.CalculateDiagram(), "mainBranchName: \"a\\\"b\"");
        }

        [TestMethod]
        public void Config_EmptyStringValue_IsNotWritten()
        {
            var graph = new GitGraph(config: new GitGraphConfig(ConfigTheme.Dark, mainBranchName: ""));

            var result = graph.CalculateDiagram();

            Assert.IsFalse(result.Contains("mainBranchName"), result);
        }

        [TestMethod]
        public void PieSlice_Label_EscapesQuotes()
        {
            var slice = new PieSlice("The \"best\"", 10);

            Assert.AreEqual("\"The #quot;best#quot;\" : 10", slice.ToString());
        }

        #endregion
    }
}
