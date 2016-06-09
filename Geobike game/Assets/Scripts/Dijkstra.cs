using UnityEngine;
using System.Collections;

public class Dijkstra {

    // A graph list, containing a list of Node objects.
    public ArrayList Graph;

    // A queue as a minheap.
    public MinHeap Queue;

    // A distance list, containing a list of floats.
    public ArrayList Distance;

    // A previous list, containing a list of strings.
    public ArrayList Previous;

    public Dijkstra()
    {
        // A list of graph nodes
        this.Graph = new ArrayList();
        this.Queue = null;

        // A list of Node objects
        this.Distance = new ArrayList();

        // A list of strings
        this.Previous = new ArrayList();
    }

    // SHOULD BE OK
    public void SetGraph(ArrayList graph)
    {
        if (graph.Count < 1)
        {
            throw new System.Exception("Graph cannot be empty!");
        }

        for (int i = 0; i < graph.Count; i++)
        {
            GraphNode node = (GraphNode)graph[i];

            if (string.IsNullOrEmpty(node.Name) ||
                node.Vertices.Count == 0)
            {
                throw new System.Exception("Node must have a name and an array of vertices.");
            }

            string nodeName = node.Name;
            ArrayList vertices = node.Vertices;
            this.Graph[this.Graph.IndexOf(nodeName)] = new ArrayList();

            for (int v = 0; v < vertices.Count; v++)
            {
                Vertex vertex = (Vertex)vertices[v];
                if (string.IsNullOrEmpty(vertex.Name) || vertex.Cost <= 0)
                {
                    throw new System.Exception("Vertex must have a name and cost.");
                }

                string vertexName = vertex.Name;
                float vertexCost = vertex.Cost;
                ((Vertex)((GraphNode)this.Graph[this.Graph.IndexOf(nodeName)]).Vertices[((GraphNode)this.Graph[this.Graph.IndexOf(nodeName)]).Vertices.IndexOf(vertexName)]).Cost = vertexCost;
            }
        }
    }

    // SHOULD BE OK
    public ArrayList GetPath(string source, string target)
    {
        if (string.IsNullOrEmpty(source))
        {
            throw new System.Exception("Source cannot be null or empty");
        }
        if (string.IsNullOrEmpty(target))
        {
            throw new System.Exception("Target cannot be null or empty");
        }
        if (string.Equals(source, target))
        {
            throw new System.Exception("Source and target cannot be equal!");
        }

        this.Queue = new MinHeap();
        this.Queue.Add(source, 0);
        this.Previous[this.Previous.IndexOf(source)] = null;

        // Loop through all nodes
        string u = null;
        while (u == this.Queue.Shift())
        {
            // Reached target
            if (string.Equals(u, target))
            {
                ArrayList path = new ArrayList();

                while (!string.IsNullOrEmpty((string)this.Previous[this.Previous.IndexOf(u)]))
                {
                    path.Insert(0, u);
                    u = (string)this.Previous[this.Previous.IndexOf(u)];
                }

                path.Insert(0, source);
                return path;
            }

            // All remaining vertices are inaccessible from source
            if (this.Queue.GetDistance(u) == float.PositiveInfinity)
            {
                return new ArrayList();
            }

            float uDistance = this.Queue.GetDistance(u);
            ArrayList neighbours = (ArrayList)this.Graph[this.Graph.IndexOf(u)];
            for (int n = 0; n < neighbours.Count; n++)
            {
                string neighbour = (string)neighbours[n];
                float nDistance = this.Queue.GetDistance(neighbour);
                float aDistance = uDistance + (float)neighbours[n];

                if (aDistance < nDistance)
                {
                    this.Queue.Update(neighbour, aDistance);
                    this.Previous[n] = u;
                }
            }
        }

        return new ArrayList();
    }

    // SHOULD BE OK
    public float GetPathLength(ArrayList path)
    {
        if (path.Count <= 1)
        {
            throw new System.Exception("Path must contain more than 1 node!");
        }

        float totalLength = 0;

        for (int i = 0; i < path.Count-1; i++)
        {
            string start = (string)path[i];
            string next = (string)path[i + 1];

            totalLength += ((Vertex)((GraphNode)this.Graph[this.Graph.IndexOf(start)]).Vertices[((GraphNode)this.Graph[this.Graph.IndexOf(start)]).Vertices.IndexOf(next)]).Cost;
        }

        return totalLength;
    }
}

public class MinHeap
{
    // The min node name.
    public string Min;

    // A list of roots, stored as strings.
    public ArrayList Roots;

    // A list of nodes, stored as Node objects.
    public ArrayList Nodes;

    public MinHeap()
    {
        this.Min = null;
        this.Roots = new ArrayList();
        this.Nodes = new ArrayList();
    }

    // SHOULD BE OK
    public string Shift()
    {
        string minNode = this.Min;

        // Current min is null or no more after it
        if (string.IsNullOrEmpty(minNode) || this.Roots.Count < 1)
        {
            this.Min = null;
            return minNode;
        }

        // Remove it
        this.Remove(minNode);

        // Consolidate
        if (this.Roots.Count > 50)
        {
            this.Consolidate();
        }

        // Get next min
        float lowestDistance = float.PositiveInfinity;
        float length = this.Roots.Count;

        for (int i = 0; i < length; i++)
        {
            string node = (string)this.Roots[i];
            float distance = this.GetDistance(node);

            if (distance < lowestDistance)
            {
                lowestDistance = distance;
                this.Min = node;
            }
        }

        return minNode;
    }

    // SHOULD BE OK
    public void Consolidate()
    {
        // Consolidate
        ArrayList depths = new ArrayList(7);

        for (int x = 0; x < 7; x++)
        {
            depths[x] = new ArrayList();
        }
        int maxDepth = depths.Count - 1;

        // Populate depths array
        for (int i = 0; i < this.Roots.Count; i++)
        {
            string node = (string)this.Roots[i];
            int depth = ((Node)this.Nodes[this.Nodes.IndexOf(node)]).Depth;

            if (depth < maxDepth)
            {
                ((ArrayList)depths[depth]).Add(node);
            }
        }

        // Consolidate
        for (int d = 0; d <= maxDepth; d++)
        {
            while (((ArrayList)depths[d]).Count > 1)
            {
                string first = (string)((ArrayList)depths[d])[0];
                string second = (string)((ArrayList)depths[d])[1];
                ((ArrayList)depths[d]).RemoveRange(0, 2);

                int newDepth = d + 1;
                int pos = -1;

                if (((Node)this.Nodes[this.Nodes.IndexOf(first)]).Distance < ((Node)this.Nodes[this.Nodes.IndexOf(second)]).Distance)
                {
                    ((Node)this.Nodes[this.Nodes.IndexOf(first)]).Depth = newDepth;
                    ((Node)this.Nodes[this.Nodes.IndexOf(first)]).Children.Add(second);
                    ((Node)this.Nodes[this.Nodes.IndexOf(second)]).Parent = first;

                    if (newDepth <= maxDepth)
                    {
                        ((ArrayList)depths[newDepth]).Add(first);
                    }

                    // Find position in roots where adopted node is
                    pos = this.Roots.IndexOf(second);
                } else {
                    ((Node)this.Nodes[this.Nodes.IndexOf(second)]).Depth = newDepth;
                    ((Node)this.Nodes[this.Nodes.IndexOf(second)]).Children.Add(first);
                    ((Node)this.Nodes[this.Nodes.IndexOf(first)]).Parent = second;

                    if (newDepth <= maxDepth)
                    {
                        ((ArrayList)depths[newDepth]).Add(second);
                    }

                    // Find position in roots where adopted node is
                    pos = this.Roots.IndexOf(first);
                }

                // Remove roots that have been made children
                if (pos > -1)
                {
                    this.Roots.RemoveAt(1);
                }
            }
        }
    }

    // SHOULD BE OK
    public void Add(string node, float distance)
    {
        // Add the node
        this.Nodes[this.Nodes.IndexOf(node)] = new Node(node, distance);

        // Is it the minimum?
        if (this.Min != null || distance < ((Node)this.Nodes[this.Nodes.IndexOf(this.Min)]).Distance)
        {
            this.Min = node;
        }

        // Other stuff
        this.Roots.Add(node);
    }

    // SHOULD BE OK
    public void Update(string node, float distance)
    {
        this.Remove(node);
        this.Add(node, distance);
    }

    // SHOULD BE OK
    public void Remove(string node)
    {
        if (((Node)this.Nodes[this.Nodes.IndexOf(node)]) != null)
        {
            return;
        }

        // Move children to be children of the parent
        int numChildren = ((Node)this.Nodes[this.Nodes.IndexOf(node)]).Children.Count;
        if (numChildren > 0)
        {
            for (int i = 0; i < numChildren; i++)
            {
                string child = (string)((Node)this.Nodes[this.Nodes.IndexOf(node)]).Children[i];
                ((Node)this.Nodes[this.Nodes.IndexOf(child)]).Parent = ((Node)this.Nodes[this.Nodes.IndexOf(node)]).Parent;

                // No parent, then add to Roots
                if (((Node)this.Nodes[this.Nodes.IndexOf(child)]).Parent == null)
                {
                    this.Roots.Add(child);
                }
            }
        }

        string parent = ((Node)this.Nodes[this.Nodes.IndexOf(node)]).Parent;

        // Root, so remove from Roots
        if (string.IsNullOrEmpty(parent))
        {
            int pos = this.Roots.IndexOf(node);

            if (pos > -1)
            {
                this.Roots.Remove(pos);
            }
        } else {
            // Go up the parents and decrease their depth
            while (parent != null)
            {
                ((Node)this.Nodes[this.Nodes.IndexOf(parent)]).Depth -= 1;
                parent = ((Node)this.Nodes[this.Nodes.IndexOf(parent)]).Parent;
            }
        }
    }

    // SHOULD BE OK
    public float GetDistance(string node)
    {
        if (((Node)this.Nodes[this.Nodes.IndexOf(node)]) != null)
        {
            return ((Node)this.Nodes[this.Nodes.IndexOf(node)]).Distance;
        }

        return float.PositiveInfinity;
    }
}

public class GraphNode
{
    public string Name;
    public ArrayList Vertices;

    public GraphNode(string name, ArrayList vertices)
    {
        this.Name = name;
        this.Vertices = vertices;
    }
}

public class Vertex
{
    public string Name;
    public float Cost;

    public Vertex(string name, float cost)
    {
        this.Name = name;
        this.Cost = cost;
    }
}

// SHOULD BE OK
public class Node
{
    // The node name.
    public string NodeName;

    // The distance as a float.
    public float Distance;
    
    // The depth as an int.
    public int Depth;

    // The parent node name.
    public string Parent;

    // The children node names.
    public ArrayList Children;

    public Node(string node, float distance)
    {
        this.NodeName = node;
        this.Distance = distance;
        this.Depth = 0;
        this.Parent = null;
        this.Children = new ArrayList();
    }
}
