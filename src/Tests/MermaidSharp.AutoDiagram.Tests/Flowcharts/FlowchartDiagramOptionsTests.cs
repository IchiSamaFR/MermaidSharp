using MermaidSharp.AutoDiagram.Diagrams;
using MermaidSharp.AutoDiagram.Options;
using MermaidSharp.AutoDiagram.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MermaidSharp.AutoDiagram.Tests.Flowcharts
{
    /// <summary>
    /// Tests for the options of <see cref="FlowchartDiagramExtension"/>.
    /// </summary>
    [TestClass]
    public class FlowchartDiagramOptionsTests
    {
        private static readonly Assembly[] Assemblies =
        {
            typeof(FlowchartDiagramOptionsTests).Assembly,
            typeof(Person).Assembly
        };

        [TestMethod]
        public void ToMermaidFlowchartDiagram_WritesNoSubgraph_WhenGroupLabelIsEmpty()
        {
            // Arrange
            var options = new FlowchartDiagramOptions { SubgraphGroupLabel = string.Empty };

            // Act
            var result = Assemblies.ToMermaidFlowchartDiagram(options).CalculateDiagram();

            // Assert
            Assert.IsFalse(result.Contains("subgraph"), result);
            StringAssert.Contains(result, "MermaidSharp.AutoDiagram.Tests.Models[MermaidSharp.AutoDiagram.Tests.Models]");
        }

        [TestMethod]
        public void ToMermaidFlowchartDiagram_UsesCustomGroupLabel()
        {
            // Arrange
            var options = new FlowchartDiagramOptions { SubgraphGroupLabel = "Solution" };

            // Act
            var result = Assemblies.ToMermaidFlowchartDiagram(options).CalculateDiagram();

            // Assert
            StringAssert.Contains(result, "subgraph Solution");
            Assert.IsFalse(result.Contains("subgraph Project"), result);
        }

        [TestMethod]
        public void ToMermaidFlowchartDiagram_Throws_WhenAssembliesIsNull()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => ((IEnumerable<Assembly>)null).ToMermaidFlowchartDiagram());
        }
    }
}
