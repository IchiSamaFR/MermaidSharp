using MermaidSharp.Configs;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Tests.GitGraphs
{
    [TestClass]
    public class AdvancedFeaturesTests
    {
        #region Basic GitGraph

        [TestMethod]
        public void GitGraph_NoConfig()
        {
            //Arrange
            var graph = new GitGraph();
            graph.Commit("c1");

            string expected = @"gitGraph
    commit id: ""c1""";

            //Act
            string result = graph.CalculateDiagram();

            //Assert
            Assert.IsNotNull(graph);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitGraph_EmptyNoConfig()
        {
            //Arrange
            var graph = new GitGraph();

            string expected = @"gitGraph";

            //Act
            string result = graph.CalculateDiagram();

            //Assert
            Assert.IsNotNull(graph);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void GitGraph_EmptyConfig()
        {
            //Arrange
            var config = new GitGraphConfig(); // All properties null/None
            var graph = new GitGraph("", config);
            graph.Commit("c1");

            string expected = @"gitGraph
    commit id: ""c1""";

            //Act
            string result = graph.CalculateDiagram();

            //Assert
            Assert.IsNotNull(graph);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitGraph_EmptyConfigNoActions()
        {
            //Arrange
            var config = new GitGraphConfig(ConfigTheme.Neutral);
            var graph = new GitGraph("", config);

            string expected = @"---
config:
    theme: neutral
---
gitGraph";

            //Act
            string result = graph.CalculateDiagram();

            //Assert
            Assert.IsNotNull(graph);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Complex Workflows

        [TestMethod]
        public void GitGraph_Complete()
        {
            //Arrange
            var graph = new GitGraph();
            graph.Commit("c1");
            graph.Branch("develop");
            graph.Checkout("develop");
            graph.Commit("c2");
            graph.Checkout("main");
            graph.Merge("develop");

            string expected = @"gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    checkout main
    merge develop";

            //Act
            string result = graph.CalculateDiagram();

            //Assert
            Assert.IsNotNull(graph);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitGraph_CompleteWithTheme()
        {
            //Arrange
            var config = new GitGraphConfig(ConfigTheme.Dark)
            {
                ShowCommitLabel = true,
                MainBranchName = "master"
            };
            var graph = new GitGraph("", config);
            graph.Commit("c1");
            graph.Branch("develop");
            graph.Checkout("develop");
            graph.Commit("c2");
            graph.Commit("c3");
            graph.Checkout("master");
            graph.Merge("develop");

            string expected = @"---
config:
    theme: dark
    gitGraph:
        showCommitLabel: true
        mainBranchName: master
---
gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    commit id: ""c3""
    checkout master
    merge develop";

            //Act
            string result = graph.CalculateDiagram();

            //Assert
            Assert.IsNotNull(graph);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitGraph_CompleteWithConfig()
        {
            //Arrange
            var config = new GitGraphConfig
            {
                ShowBranches = false,
                MainBranchName = "master"
            };
            var graph = new GitGraph("", config);
            graph.Commit("c1");
            graph.Branch("develop");
            graph.Checkout("develop");
            graph.Commit("c2");
            graph.Commit("c3");
            graph.Checkout("master");
            graph.Merge("develop");

            string expected = @"---
config:
    gitGraph:
        showBranches: false
        mainBranchName: master
---
gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    commit id: ""c3""
    checkout master
    merge develop";

            //Act
            string result = graph.CalculateDiagram();

            //Assert
            Assert.IsNotNull(graph);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        #endregion
    }
}