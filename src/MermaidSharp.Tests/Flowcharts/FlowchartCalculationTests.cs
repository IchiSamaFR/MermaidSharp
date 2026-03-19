using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.Flowcharts
{
    [TestClass]
    public class FlowchartCalculationTests
    {
        [TestMethod]
        public void ValidTitleFlowchart()
        {
            // Arrange
            string title = "My Flowchart Title";
            string direction = "LR";
            var flowchart = new FlowchartDiagram(title, direction);
            string expected = @"---
title: My Flowchart Title
---
flowchart LR";

            // Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ValidTDFlowchart()
        {
            //Arrange
            string direction = "LR";
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            string expected = @"flowchart LR";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SingleNodeFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1[This is node 1]";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TwoNodesFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1"),
                new FlowNode("node2", "This is node 2")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TwoNodesRoundedNodesFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1", FlowNodeShapeType.Rounded),
                new FlowNode("node2", "This is node 2", FlowNodeShapeType.Rounded)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1(This is node 1)
    node2(This is node 2)";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TwoNodesAndLinkFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1"),
                new FlowNode("node2", "This is node 2")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", "")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1-->node2";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TwoNodesAndLinkTextFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1"),
                new FlowNode("node2", "This is node 2")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", "link text!")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1--link text!-->node2";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TwoNodesAndBidirectionalLinkTextFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1"),
                new FlowNode("node2", "This is node 2")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", "link text!", isBidirectional: true)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1<--link text!-->node2";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ThreeNodesAndTwoLinksFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1"),
                new FlowNode("node2", "This is node 2"),
                new FlowNode("node3", "This is node 3")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", "12s"),
                new FlowLink("node1", "node3", "3mins")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node3[This is node 3]
    node1--12s-->node2
    node1--3mins-->node3";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ThreeNodesAndTwoLinksWithMultipleColorsFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1"),
                new FlowNode("node2", "This is node 2"),
                new FlowNode("node3", "This is node 3")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", "12s", "stroke-width:4px,stroke:red"),
                new FlowLink("node1", "node3", "3mins")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node3[This is node 3]
    node1--12s-->node2
    node1--3mins-->node3
    linkStyle 0 stroke-width:4px,stroke:red";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TwoNodesInASubGraphFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowSubGraph> subGraphs = new List<FlowSubGraph>()
            {
                new FlowSubGraph("graph1",
                    new List<FlowNode>
                    {
                        new FlowNode("node1", "This is node 1"),
                        new FlowNode("node2", "This is node 2")
                    },
                    new List<FlowLink>())
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.SubGraphs.AddRange(subGraphs);

            string expected = @"flowchart LR
    subgraph graph1
    node1[This is node 1]
    node2[This is node 2]
    end";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FourNodesInTwoSubGraphsFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowSubGraph> subGraphs = new List<FlowSubGraph>()
            {
                new FlowSubGraph("graph1",
                    new List<FlowNode>
                    {
                        new FlowNode("node1", "This is node 1"),
                        new FlowNode("node2", "This is node 2")
                    },
                    new List<FlowLink>()),
                new FlowSubGraph("graph2",
                    new List<FlowNode>
                    {
                        new FlowNode("node3", "This is node 3"),
                        new FlowNode("node4", "This is node 4")
                    },
                    new List<FlowLink> {
                        new FlowLink("node1", "node3")
                    })
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.SubGraphs.AddRange(subGraphs);

            string expected = @"flowchart LR
    subgraph graph1
    node1[This is node 1]
    node2[This is node 2]
    end
    subgraph graph2
    node3[This is node 3]
    node4[This is node 4]
    node1-->node3
    end";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FourNodesInTwoSubGraphsAndTwoSingleNodesFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node5", "This is node 5"),
                new FlowNode("node6", "This is node 6"),
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node5", "node6")
            };
            List<FlowSubGraph> subGraphs = new List<FlowSubGraph>()
            {
                new FlowSubGraph("graph1",
                    new List<FlowNode>
                    {
                        new FlowNode("node1", "This is node 1"),
                        new FlowNode("node2", "This is node 2")
                    },
                    new List<FlowLink>()),
                new FlowSubGraph("graph2",
                    new List<FlowNode>
                    {
                        new FlowNode("node3", "This is node 3"),
                        new FlowNode("node4", "This is node 4")
                    },
                    new List<FlowLink> {
                        new FlowLink("node1", "node3")
                    })
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.SubGraphs.AddRange(subGraphs);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    subgraph graph1
    node1[This is node 1]
    node2[This is node 2]
    end
    subgraph graph2
    node3[This is node 3]
    node4[This is node 4]
    node1-->node3
    end
    node5[This is node 5]
    node6[This is node 6]
    node5-->node6";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FourNodesInTwoSubGraphsWithDirectionAndTwoSingleNodesFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node5", "This is node 5"),
                new FlowNode("node6", "This is node 6"),
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node5", "node6")
            };
            List<FlowSubGraph> subGraphs = new List<FlowSubGraph>()
            {
                new FlowSubGraph("graph1",
                    new List<FlowNode>
                    {
                        new FlowNode("node1", "This is node 1"),
                        new FlowNode("node2", "This is node 2")
                    },
                    new List<FlowLink>(),
                    direction),
                new FlowSubGraph("graph2",
                    new List<FlowNode>
                    {
                        new FlowNode("node3", "This is node 3"),
                        new FlowNode("node4", "This is node 4")
                    },
                    new List<FlowLink> {
                        new FlowLink("node1", "node3")
                    },
                    direction)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.SubGraphs.AddRange(subGraphs);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    subgraph graph1
    direction LR
    node1[This is node 1]
    node2[This is node 2]
    end
    subgraph graph2
    direction LR
    node3[This is node 3]
    node4[This is node 4]
    node1-->node3
    end
    node5[This is node 5]
    node6[This is node 6]
    node5-->node6";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DifferentNodeShapesFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1", FlowNodeShapeType.Rectangle),
                new FlowNode("node2", "This is node 2", FlowNodeShapeType.Rounded),
                new FlowNode("node3", "This is node 3", FlowNodeShapeType.Stadium),
                new FlowNode("node4", "This is node 4", FlowNodeShapeType.Cylinder),
                new FlowNode("node5", "This is node 5", FlowNodeShapeType.Circle),
                new FlowNode("node6", "This is node 6", FlowNodeShapeType.Rhombus),
                new FlowNode("node7", "This is node 7", FlowNodeShapeType.Hexagon)
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart LR
    node1[This is node 1]
    node2(This is node 2)
    node3([This is node 3])
    node4[(This is node 4)]
    node5((This is node 5))
    node6{This is node 6}
    node7{{This is node 7}}";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void SubgraphsWithDirectionsFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowSubGraph> subGraphs = new List<FlowSubGraph>()
            {
                new FlowSubGraph("graph1",
                    new List<FlowNode>
                    {
                        new FlowNode("node1", "This is node 1"),
                        new FlowNode("node2", "This is node 2")
                    },
                    new List<FlowLink>(),
                    "TB"),
                new FlowSubGraph("graph2",
                    new List<FlowNode>
                    {
                        new FlowNode("node3", "This is node 3"),
                        new FlowNode("node4", "This is node 4")
                    },
                    new List<FlowLink> {
                        new FlowLink("node1", "node3")
                    },
                    "BT")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.SubGraphs.AddRange(subGraphs);

            string expected = @"flowchart LR
    subgraph graph1
    direction TB
    node1[This is node 1]
    node2[This is node 2]
    end
    subgraph graph2
    direction BT
    node3[This is node 3]
    node4[This is node 4]
    node1-->node3
    end";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinkStylesFlowchart()
        {
            //Arrange
            string direction = "LR";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1"),
                new FlowNode("node2", "This is node 2"),
                new FlowNode("node3", "This is node 3")
            };
            List<FlowLink> links = new List<FlowLink>()
            {
                new FlowLink("node1", "node2", "12s", "stroke-width:4px,stroke:red"),
                new FlowLink("node1", "node3", "3mins", "stroke-width:2px,stroke:blue")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);
            flowchart.Links.AddRange(links);

            string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node3[This is node 3]
    node1--12s-->node2
    node1--3mins-->node3
    linkStyle 0 stroke-width:4px,stroke:red
    linkStyle 1 stroke-width:2px,stroke:blue";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ValidBTDirectionFlowchart()
        {
            //Arrange
            string direction = "BT";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart BT
    node1[This is node 1]";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ValidRLDirectionFlowchart()
        {
            //Arrange
            string direction = "RL";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart RL
    node1[This is node 1]";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ValidTBDirectionFlowchart()
        {
            //Arrange
            string direction = "TB";
            List<FlowNode> nodes = new List<FlowNode>()
            {
                new FlowNode("node1", "This is node 1")
            };
            FlowchartDiagram flowchart = new FlowchartDiagram(direction);
            flowchart.Nodes.AddRange(nodes);

            string expected = @"flowchart TB
    node1[This is node 1]";

            //Act
            string result = flowchart.CalculateDiagram();

            //Assert
            Assert.IsNotNull(flowchart);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
