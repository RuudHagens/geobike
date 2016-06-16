using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dijkstra {

    // A graph list, containing a list of Node objects.
    //public List<GraphNode> Graph;
    public ArrayList Graph;

    // A queue as a minheap.
    public MinHeap Queue;

    // A distance list, containing a list of floats.
    public ArrayList Distance;

    // A previous list, containing a list of strings.
    Dictionary<string, string> Previous;

    public Dijkstra()
    {
        // A list of graph nodes
        this.Graph = new ArrayList();
        //this.Graph = new List<GraphNode>();
        this.Queue = null;

        // A list of Node objects
        this.Distance = new ArrayList();

        // A list of strings
        this.Previous = new Dictionary<string, string>();
    }

    //public void SetGraph(List<GraphNode> graph)
    public void SetGraph(ArrayList graph)
    {
        this.Graph = graph;
    }

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
        this.Previous[source] = null;

        // Loop through all nodes
        string u = null;
        do
        {
            u = this.Queue.Shift();

            if(u == null)
            {
                break;
            }

            // Reached target
            if (string.Equals(u, target))
            {
                ArrayList path = new ArrayList();

                //return path;
                while(this.Previous[u] != null)
                {
                    path.Insert(0, u);
                    u = this.Previous[u];
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

            int indexFoundNode = -1;
            for (int i = 0; i < Graph.Count; i++)
            {
                if (string.Equals(((GraphNode)Graph[i]).Name, u))
                {
                    indexFoundNode = i;
                }
            }
            if (indexFoundNode == -1)
            {
                break;
            }

            ArrayList neighbours = (ArrayList)((GraphNode)(this.Graph[indexFoundNode])).Vertices;
            foreach (Vertex neighbour in neighbours)
            {
                float nDistance = this.Queue.GetDistance(neighbour.Name);
                float aDistance = uDistance + neighbour.Cost;

                if(aDistance < nDistance)
                {
                    this.Queue.Update(neighbour.Name, aDistance);

                    this.Previous[neighbour.Name] = u;
                }
            }
        }
        while (u != null);

        return new ArrayList();
    }

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
            //totalLength += ((Vertex)((GraphNode)this.Graph[this.Graph.IndexOf(this.Graph.Find(j => j.Name == start))]).Vertices[((GraphNode)this.Graph[this.Graph.IndexOf(this.Graph.Find(j => j.Name == start))]).Vertices.IndexOf(next)]).Cost;
        }

        return totalLength;
    }

    /// <summary>
    /// Method to get a list of nodes around a given node. Returns
    /// null when the nodename has not been found in the graph.
    /// </summary>
    /// <param name="nodeName">The nodename as a string.</param>
    /// <returns>Returns a list of nodenames.</returns>
    public List<string> GetNodesAroundNode(string nodeName)
    {
        // Get the index of the nodename in this.Graph
        int indexOfNode = this.Graph.IndexOf(nodeName);
        //int indexOfNode = this.Graph.IndexOf(this.Graph.Find(i => i.Name == nodeName));

        // Check if the index >= 0 (which is true when the nodename has been found)
        if (indexOfNode >= 0)
        {
            // Create an empty list to contain the names of neighbour nodes
            List<string> returnedList = new List<string>();

            // Loop through all vertices
            //foreach (Vertex v in this.Graph[indexOfNode].Vertices)
            foreach (Vertex v in (ArrayList)this.Graph[indexOfNode])
            {
                // Add the name of the vertex to returnedList
                returnedList.Add(v.Name);
            }

            // Return the list of neighbour nodes
            return returnedList;
        }

        // Return null when nothing has been found
        return null;
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

    public void Add(string node, float distance)
    {
        Node newNode = new Node(node, distance);

        int indexFoundNode = -1;
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (((Node)Nodes[i]).NodeName == node)
            {
                indexFoundNode = i;
            }
        }

        // Add the node
        if (indexFoundNode == -1)
        {
            this.Nodes.Add(newNode);
        }
        else
        {
            this.Nodes[indexFoundNode] = newNode;
        }

        // Is it the minimum?
        if (this.Min == null)
        {
            this.Min = node;
        }
        else
        {
            indexFoundNode = -1;
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (((Node)Nodes[i]).NodeName == node)
                {
                    indexFoundNode = i;
                }
            }

            // Add the node
            if (indexFoundNode != -1)
            {
                if (distance < ((Node)this.Nodes[indexFoundNode]).Distance)
                {
                    this.Min = node;
                }
            }           
        }

        // Other stuff
        this.Roots.Add(node);
    }

    public void Update(string node, float distance)
    {
        this.Remove(node);
        this.Add(node, distance);
    }

    public void Remove(string node)
    {
        int indexFoundNode = -1;
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (((Node)Nodes[i]).NodeName == node)
            {
                indexFoundNode = i;
            }
        }

        // Move children to be children of the parent
        if (indexFoundNode == -1)
        {
            return;
        }

        int numChildren = ((Node)this.Nodes[indexFoundNode]).Children.Count;
        if (numChildren > 0)
        {
            for (int i = 0; i < numChildren; i++)
            {
                string child = (string)((Node)this.Nodes[indexFoundNode]).Children[i];

                int indexFoundChild = -1;
                for (int j = 0; j < Nodes.Count; j++)
                {
                    if (((Node)Nodes[j]).NodeName == node)
                    {
                        indexFoundChild = j;
                    }
                }
                if (indexFoundChild == -1)
                {
                    break;
                }

                ((Node)this.Nodes[indexFoundChild]).Parent = ((Node)this.Nodes[indexFoundNode]).Parent;

                // No parent, then add to Roots
                if (((Node)this.Nodes[indexFoundChild]).Parent == null)
                {
                    this.Roots.Add(child);
                }
            }
        }

        string parent = ((Node)this.Nodes[indexFoundNode]).Parent;

        // Root, so remove from Roots
        if (string.IsNullOrEmpty(parent))
        {
            int indexFoundRoot = this.Roots.IndexOf(node);
            if(indexFoundRoot != -1)
            {
                this.Roots.RemoveAt(indexFoundRoot);
            }
        }
        else {
            // Go up the parents and decrease their depth
            while (parent != null)
            {
                int indexFoundParent = -1;
                for (int i = 0; i < Nodes.Count; i++)
                {
                    if (((Node)Nodes[i]).NodeName == node)
                    {
                        indexFoundParent = i;
                    }
                }
                if (indexFoundParent == -1)
                {
                    break;
                }

                ((Node)this.Nodes[indexFoundParent]).Depth -= 1;
                parent = ((Node)this.Nodes[indexFoundParent]).Parent;
            }
        }
    }

    // SHOULD BE OK
    public float GetDistance(string node)
    {
        int indexFoundNode = -1;
        for (int i = 0; i < Nodes.Count; i++)
        {
            if (((Node)Nodes[i]).NodeName == node)
            {
                indexFoundNode = i;
            }
        }

        if (indexFoundNode != -1)
        {
            if (((Node)this.Nodes[indexFoundNode]) != null)
            {
                return ((Node)this.Nodes[indexFoundNode]).Distance;
            }
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
