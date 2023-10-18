namespace DSA.TreesAndGraphs;


using System;
using System.Collections.Generic;

class Graph
{
    private Dictionary<int, List<int>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<int, List<int>>();
    }

    public void AddVertex(int vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
            adjacencyList[vertex] = new List<int>();
    }

    public void AddEdge(int source, int destination)
    {
        if (!adjacencyList.ContainsKey(source))
            AddVertex(source);
        if (!adjacencyList.ContainsKey(destination))
            AddVertex(destination);

        adjacencyList[source].Add(destination);
        adjacencyList[destination].Add(source); // For undirected graphs
    }

    public List<int> GetNeighbors(int vertex)
    {
        if (adjacencyList.ContainsKey(vertex))
            return adjacencyList[vertex];
        else
            return new List<int>();
    }
}

public static class GraphProgram
{
    public static void Apply()
    {
        Graph graph = new Graph();

        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(2, 4);
        graph.AddEdge(3, 5);

        Console.WriteLine("Neighbors of vertex 2: " + string.Join(", ", graph.GetNeighbors(2)));
    }
}
