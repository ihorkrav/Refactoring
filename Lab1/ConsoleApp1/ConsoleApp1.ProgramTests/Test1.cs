namespace ConsoleApp1.ProgramTests;

using ConsoleApp1.Program;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;


  [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void TestAddNode()
        {
            // Arrange
            var graph = new ProgramCode.Graph();

            // Act
            graph.AddNode(1);

            // Assert
            Assert.IsTrue(graph.AdjacencyList.ContainsKey(1));
            Assert.AreEqual(0, graph.AdjacencyList[1].Count);
        }

        [TestMethod]
        public void TestAddEdge()
        {
            // Arrange
            var graph = new ProgramCode.Graph();

            // Act
            graph.AddEdge(1, 2);

            // Assert
            Assert.IsTrue(graph.AdjacencyList.ContainsKey(1));
            Assert.IsTrue(graph.AdjacencyList.ContainsKey(2));
            Assert.IsTrue(graph.AdjacencyList[1].Contains(2));
            Assert.IsTrue(graph.AdjacencyList[2].Contains(1)); // Since it's undirected
        }

        [TestMethod]
        public void TestGraphInitializationEmpty()
        {
            // Arrange
            var graph = new ProgramCode.Graph();

            // Assert
            Assert.AreEqual(0, graph.AdjacencyList.Count);
        }

        [TestMethod]
        public void TestAddMultipleNodes()
        {
            // Arrange
            var graph = new ProgramCode.Graph();

            // Act
            graph.AddNode(1);
            graph.AddNode(2);
            graph.AddNode(3);

            // Assert
            Assert.AreEqual(3, graph.AdjacencyList.Count);
        }

        [TestMethod]
        public void TestAddingDuplicateNodes()
        {
            // Arrange
            var graph = new ProgramCode.Graph();

            // Act
            graph.AddNode(1);
            graph.AddNode(1);  // Add duplicate

            // Assert
            Assert.AreEqual(1, graph.AdjacencyList.Count);
        }

        [TestMethod]
        public void TestBFS()
        {
            // Arrange
            var graph = new ProgramCode.Graph();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 5);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                graph.BFS(1);

                // Assert - BFS should visit nodes in level order
                var output = sw.ToString().Trim();
                Assert.IsTrue(output.Contains("1 2 3 4 5"));
            }
        }

        [TestMethod]
        public void TestDFS()
        {
            // Arrange
            var graph = new ProgramCode.Graph();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 5);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                graph.DFS(1);

                // Assert - DFS should visit nodes in depth order
                var output = sw.ToString().Trim();
                Assert.IsTrue(output.Contains("1 2 4 3 5") || output.Contains("1 3 5 2 4"));
            }
        }

        [TestMethod]
        public void TestBFSWithNonExistentNode()
        {
            // Arrange
            var graph = new ProgramCode.Graph();
            graph.AddEdge(1, 2);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                graph.BFS(99);  // Node 99 does not exist

                // Assert - No output expected
                var output = sw.ToString().Trim();
                Assert.AreEqual("Start node not found in the graph.", output);
            }
        }

        [TestMethod]
        public void TestDFSWithNonExistentNode()
        {
            // Arrange
            var graph = new ProgramCode.Graph();
            graph.AddEdge(1, 2);

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                graph.DFS(99);  // Node 99 does not exist

                // Assert - No output expected
                var output = sw.ToString().Trim();
                Assert.AreEqual("Start node not found in the graph.", output);
            }
        }

        [TestMethod]
        public void TestGraphHasCorrectEdges()
        {
            // Arrange
            var graph = new ProgramCode.Graph();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);

            // Assert
            Assert.IsTrue(graph.AdjacencyList[1].Contains(2));
            Assert.IsTrue(graph.AdjacencyList[2].Contains(1));
            Assert.IsTrue(graph.AdjacencyList[2].Contains(3));
            Assert.IsTrue(graph.AdjacencyList[3].Contains(2));
            Assert.IsTrue(graph.AdjacencyList[3].Contains(4));
            Assert.IsTrue(graph.AdjacencyList[4].Contains(3));
        }
    }
