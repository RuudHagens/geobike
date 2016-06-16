using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class is used to calculate the shortest path between two nodes, given a graph map.
/// </summary>
public class Dijkstra {

    /// <summary>
    /// A graph list, containing a list of Node objects.
    /// </summary>
    public ArrayList Graph;

    /// <summary>
    /// A queue as a minheap.
    /// </summary>
    public MinHeap Queue;

    /// <summary>
    /// A distance list, containing a list of floats.
    /// </summary>
    public ArrayList Distance;

    /// <summary>
    /// A previous list, containing a list of strings.
    /// </summary>
    Dictionary<string, string> Previous;

    /// <summary>
    /// Initializes a new instance of the <see cref="Dijkstra"/> class.
    /// </summary>
    public Dijkstra()
    {
        // A list of graph nodes.
        this.Graph = new ArrayList();

        // Set Queue to null.
        this.Queue = null;

        // A list of strings.
        this.Previous = new Dictionary<string, string>();
    }

    /// <summary>
    /// Method to set the graph of the class to the given graph.
    /// </summary>
    /// <param name="graph">The graph to set.</param>
    public void SetGraph(ArrayList graph)
    {
        this.Graph = graph;
    }

    /// <summary>
    /// Method to get the shortest path between two nodes.
    /// </summary>
    /// <param name="source">The source node as a string.</param>
    /// <param name="target">The target node as a string.</param>
    /// <returns>Returns the shortest path between the two given nodes.</returns>
    public ArrayList GetPath(string source, string target)
    {
        // Throw an exception when the source is null or empty.
        if (string.IsNullOrEmpty(source))
        {
            throw new System.Exception("Source cannot be null or empty");
        }

        // Throw an exception when the target is null or empty.
        if (string.IsNullOrEmpty(target))
        {
            throw new System.Exception("Target cannot be null or empty");
        }

        // Throw an exception when the source is the same as the target.
        if (string.Equals(source, target))
        {
            throw new System.Exception("Source and target cannot be equal!");
        }

        // Create a new MinHeap.
        this.Queue = new MinHeap();

        // Add the source at the first place.
        this.Queue.Add(source, 0);

        // Set the value of source in this.Previous to null.
        this.Previous[source] = null;

        // Loop through all nodes
        string u = null;
        do
        {
            // Set u to the first item in the queue and remove this item from the queue.
            u = this.Queue.Shift();

            // Break out of the loop when u is null.
            if (u == null) break;

            // Check if the target has been reached.
            if (string.Equals(u, target))
            {
                // Create a new list of strings as a path.
                ArrayList path = new ArrayList();

                // Loop while the key of u in this.Previous is not null.
                while (this.Previous[u] != null)
                {
                    // Insert u in path at the first place.
                    path.Insert(0, u);

                    // Set the value u to the key of u.
                    u = this.Previous[u];
                }

                // Insert the source at the first place of path.
                path.Insert(0, source);

                // Return the path.
                return path;
            }

            // Check if the remaining vertices are inaccessible from source.
            if (this.Queue.GetDistance(u) == float.PositiveInfinity)
            {
                // Return an empty list when the remaining vertices are inaccessible from source.
                return new ArrayList();
            }

            // Initialize a new float with the distance of u.
            float uDistance = this.Queue.GetDistance(u);

            // Find the index of u in this.Graph.
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

            // Get a list of neighbours.
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

    /// <summary>
    /// Method to get the length of a given path.
    /// </summary>
    /// <param name="path">A list of strings.</param>
    /// <returns>Returns the length of the path as a float.</returns>
    public float GetPathLength(ArrayList path)
    {
        // Throw an exception when there are less than 2 nodes in path.
        if (path.Count <= 1)
        {
            throw new System.Exception("Path must contain more than 1 node!");
        }

        // Float to store the total length of the given path.
        float totalLength = 0;

        // Loop through all path nodes.
        for (int i = 0; i < path.Count-1; i++)
        {
            // Store the start and end node.
            string start = (string)path[i];
            string next = (string)path[i + 1];

            // Increase the total length with the length between the two nodes.
            totalLength += ((Vertex)((GraphNode)this.Graph[this.Graph.IndexOf(start)]).Vertices[((GraphNode)this.Graph[this.Graph.IndexOf(start)]).Vertices.IndexOf(next)]).Cost;
        }

        // Return the total length of the path.
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
        // Get the index of the nodename in this.Graph.
        int indexOfNode = this.Graph.IndexOf(nodeName);

        // Check if the index >= 0 (which is true when the nodename has been found)
        if (indexOfNode >= 0)
        {
            // Create an empty list to contain the names of neighbour nodes.
            List<string> returnedList = new List<string>();

            // Loop through all vertices.
            foreach (Vertex v in (ArrayList)this.Graph[indexOfNode])
            {
                // Add the name of the vertex to returnedList/
                returnedList.Add(v.Name);
            }

            // Return the list of neighbour nodes.
            return returnedList;
        }

        // Return null when nothing has been found.
        return null;
    }
}

/// <summary>
/// This class is used as a way to improve performance of the algorithm
/// by using a fixed heap size to prevent the algorithm from using too
/// much memory.
/// </summary>
public class MinHeap
{
    /// <summary>
    /// The min node name.
    /// </summary>
    public string Min;

    /// <summary>
    /// A list of roots, stored as strings.
    /// </summary>
    public ArrayList Roots;

    /// <summary>
    /// A list of nodes, stored as Node objects.
    /// </summary>
    public ArrayList Nodes;

    public MinHeap()
    {
        this.Min = null;
        this.Roots = new ArrayList();
        this.Nodes = new ArrayList();
    }

    /// <summary>
    /// Method to shift the nodes of the heap.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// A method to consolidate the heap.
    /// </summary>
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

                    // Find position in roots where adopted node is.
                    pos = this.Roots.IndexOf(second);
                } else {
                    ((Node)this.Nodes[this.Nodes.IndexOf(second)]).Depth = newDepth;
                    ((Node)this.Nodes[this.Nodes.IndexOf(second)]).Children.Add(first);
                    ((Node)this.Nodes[this.Nodes.IndexOf(first)]).Parent = second;

                    if (newDepth <= maxDepth)
                    {
                        ((ArrayList)depths[newDepth]).Add(second);
                    }

                    // Find position in roots where adopted node is.
                    pos = this.Roots.IndexOf(first);
                }

                // Remove roots that have been made children.
                if (pos > -1)
                {
                    this.Roots.RemoveAt(1);
                }
            }
        }
    }

    /// <summary>
    /// Method to add a node to the heap.
    /// </summary>
    /// <param name="node">The node to be added.</param>
    /// <param name="distance">The distance of the node.</param>
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

    /// <summary>
    /// Method to update the distance of the node.
    /// </summary>
    /// <param name="node">The node to be updated.</param>
    /// <param name="distance">The new distance of the node.</param>
    public void Update(string node, float distance)
    {
        this.Remove(node);
        this.Add(node, distance);
    }

    /// <summary>
    /// Method to remove a node from the heap.
    /// </summary>
    /// <param name="node">The node to be removed from the heap.</param>
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

        // Move children to be children of the parent.
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

                // No parent, then add to Roots.
                if (((Node)this.Nodes[indexFoundChild]).Parent == null)
                {
                    this.Roots.Add(child);
                }
            }
        }

        string parent = ((Node)this.Nodes[indexFoundNode]).Parent;

        // Root, so remove from Roots.
        if (string.IsNullOrEmpty(parent))
        {
            int indexFoundRoot = this.Roots.IndexOf(node);
            if(indexFoundRoot != -1)
            {
                this.Roots.RemoveAt(indexFoundRoot);
            }
        }
        else {
            // Go up the parents and decrease their depth.
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

    /// <summary>
    /// Method to get the distance of a node.
    /// </summary>
    /// <param name="node">The node name.</param>
    /// <returns>Returns the distance of the node.</returns>
    public float GetDistance(string node)
    {
        // Set the default value of indexFoundNode to -1.
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

/// <summary>
/// This class is used to store information about the upper most node (graphnode).
/// </summary>
public class GraphNode
{
    /// <summary>
    /// The name of the node.
    /// </summary>
    public string Name;

    /// <summary>
    /// The list of vertices of the node.
    /// </summary>
    public ArrayList Vertices;

    /// <summary>
    /// Initializes a new instance of the <see cref="GraphNode"/> class.
    /// </summary>
    /// <param name="name">The name of the graph node.</param>
    /// <param name="vertices">The list of vertices of the graph node.</param>
    public GraphNode(string name, ArrayList vertices)
    {
        this.Name = name;
        this.Vertices = vertices;
    }
}

/// <summary>
/// This class is used to store information about a vertex.
/// </summary>
public class Vertex
{
    /// <summary>
    /// The name of the vertex.
    /// </summary>
    public string Name;

    /// <summary>
    /// The cost (length) of the vertex.
    /// </summary>
    public float Cost;

    /// <summary>
    /// Initializes a new instance of the <see cref="Vertex"/> class.
    /// </summary>
    /// <param name="name">The name of the vertex.</param>
    /// <param name="cost">The cost (length) of the vertex.</param>
    public Vertex(string name, float cost)
    {
        this.Name = name;
        this.Cost = cost;
    }
}

/// <summary>
/// This class is used to store information about a node.
/// </summary>
public class Node
{
    /// <summary>
    /// The node name.
    /// </summary>
    public string NodeName;

    /// <summary>
    /// The distance as a float.
    /// </summary>
    public float Distance;

    /// <summary>
    /// The depth as an int.
    /// </summary>
    public int Depth;

    /// <summary>
    /// The parent node name.
    /// </summary>
    public string Parent;

    /// <summary>
    /// The children node names.
    /// </summary>
    public ArrayList Children;

    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> instance.
    /// </summary>
    /// <param name="node">The name of the node.</param>
    /// <param name="distance">The distance of the node.</param>
    public Node(string node, float distance)
    {
        this.NodeName = node;
        this.Distance = distance;
        this.Depth = 0;
        this.Parent = null;
        this.Children = new ArrayList();
    }
}
