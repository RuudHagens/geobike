using UnityEngine;
using System.Collections;

public class Dijkstra {

    public ArrayList Graph;
    public ArrayList Queue;
    public ArrayList Distance;
    public ArrayList Previous;

    public Dijkstra()
    {
        this.Graph = new ArrayList();
        this.Queue = null;
        this.Distance = new ArrayList();
        this.Previous = new ArrayList();
    }

    public void SetGraph(ArrayList graph)
    {
        if (graph.Count < 1)
        {
            throw new System.Exception("Graph cannot be empty!");
        }

        for (int i = 0; i < graph.length; i++)
        {
            ArrayList node = graph[i];

            if (node.length != 2)
            {
                throw new System.Exception("Node must contain 2 values.");
            }

            string nodeName = node[0];
            ArrayList vertices = node[1];
            this.Graph[nodeName] = new ArrayList();

            for (int v = 0; v < vertices.Count; v++)
            {
                ArrayList vertex = vertices[v];
                if (vertex.Count != 2)
                {
                    throw new System.Exception("Vertex must be an array and contain 2 values.");
                }

                string vertexName = vertex[0];
                float vertexCost = vertex[1];
                this.Graph[nodeName][vertexName] = vertexCost;
            }
        }
    }

    //public ArrayList GetPath(var source, var target)
    //{

    //}
}
