

namespace DSA.Stack.UseCaseSamples;


/// <summary>
/// DFS is commonly used in graph-related problems to explore all vertices and edges of a graph.
/// </summary>

public class GraphSample
{
    private int V; // Number of vertices
    private List<int>[] adjacencyList; // Adjacency list representation of the graph

    public GraphSample(int vertices)
    {
        V = vertices;
        adjacencyList = new List<int>[vertices];
        for (int i = 0; i < vertices; i++)
        {
            adjacencyList[i] = new List<int>();
        }
    }

    public void AddEdge(int v, int w)
    {
        adjacencyList[v].Add(w);
    }

    // DFS traversal of the graph from a given source vertex
    public void DFS(int source)
    {
        bool[] visited = new bool[V]; // To keep track of visited vertices
        Stack<int> stack = new Stack<int>(); // Stack for DFS traversal

        // Mark the source vertex as visited and push it to the stack
        visited[source] = true;
        stack.Push(source);

        while (stack.Count > 0)
        {
            int currentVertex = stack.Pop();
            Console.Write(currentVertex + " "); // Process the current vertex

            // Explore all adjacent vertices of the current vertex
            foreach (int adjacentVertex in adjacencyList[currentVertex])
            {
                if (!visited[adjacentVertex])
                {
                    visited[adjacentVertex] = true;
                    stack.Push(adjacentVertex);
                }
            }
        }
    }
    public static void Apply()
    {
        // Create a sample graph
        GraphSample graph = new GraphSample(7);
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(1, 4);
        graph.AddEdge(2, 5);
        graph.AddEdge(2, 6);

        Console.WriteLine("Depth-First Traversal (starting from vertex 0):");
        graph.DFS(0);
    }
}