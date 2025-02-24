// See https://aka.ms/new-console-template for more information

namespace ConsoleApp1.Program
{
    public class ProgramCode
    {
       public class Graph
        {
        private Dictionary<int, List<int>> adjacencyList;

        public Graph()
        {
            adjacencyList = new Dictionary<int, List<int>>();
        }

        // Add a node to the graph
        public void AddNode(int node)
        {
            if (!adjacencyList.ContainsKey(node))
            {
                adjacencyList[node] = new List<int>();
            }
        }

        public Dictionary<int, List<int>> AdjacencyList
        {
            get { return adjacencyList; }
        }

        // Add an edge (connection) between two nodes
        public void AddEdge(int node1, int node2)
        {
            if (!adjacencyList.ContainsKey(node1)) AddNode(node1);
            if (!adjacencyList.ContainsKey(node2)) AddNode(node2);

            adjacencyList[node1].Add(node2);
            adjacencyList[node2].Add(node1); // Remove this for a directed graph
        }

        // Display the graph as an adjacency list
        public void PrintGraph()
        {
            foreach (var node in adjacencyList)
            {
                Console.Write(node.Key + " -> ");
                Console.WriteLine(string.Join(", ", node.Value));
            }
        }

        // Breadth-First Search (BFS)
        public void BFS(int startNode)
        {
           if (!adjacencyList.ContainsKey(startNode))
                {
                    Console.WriteLine("Start node not found in the graph.");
                    return;
                }

                HashSet<int> visited = new HashSet<int>();
                Queue<int> queue = new Queue<int>();

                visited.Add(startNode);
                queue.Enqueue(startNode);

                Console.WriteLine("\nBFS Traversal:");
                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();
                    Console.Write(current + " ");

                    foreach (int neighbor in adjacencyList[current])
                    {
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            queue.Enqueue(neighbor);
                        }
                    }
                }
                Console.WriteLine();
        }

        // Depth-First Search (DFS)
        public void DFS(int startNode)
        {
            if (!adjacencyList.ContainsKey(startNode))
                {
                    Console.WriteLine("Start node not found in the graph.");
                    return;
                }

                HashSet<int> visited = new HashSet<int>();
                Console.WriteLine("\nDFS Traversal:");
                DFSHelper(startNode, visited);
                Console.WriteLine();
        }

        private void DFSHelper(int node, HashSet<int> visited)
        {
            visited.Add(node);
            Console.Write(node + " ");

            foreach (int neighbor in adjacencyList[node])
            {
                if (!visited.Contains(neighbor))
                {
                    DFSHelper(neighbor, visited);
                }
            }
        }
        // static void Main()
        // {
        //     Graph graph = new Graph();

        //     // Adding nodes and edges
        //     graph.AddEdge(1, 2);
        //     graph.AddEdge(1, 3);
        //     graph.AddEdge(2, 4);
        //     graph.AddEdge(2, 5);
        //     graph.AddEdge(3, 6);
        //     graph.AddEdge(3, 7);

        //     // Display the graph
        //     Console.WriteLine("Graph Adjacency List:");
        //     graph.PrintGraph();

        //     // Perform BFS & DFS
        //     graph.BFS(1);
        //     graph.DFS(1);
        // }
    }

    
      
}
}

// Driver Code

  
