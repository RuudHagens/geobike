using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class is used to calculate the shortest path between two nodes, given a graph map.
/// </summary>
public class Dijkstra
{
    /// <summary>
    /// A graph list, containing a list of Node objects.
    /// </summary>
    private List<GraphNode> Graph
    {
        get;
        set;
    }

    /// <summary>
    /// A queue as a minheap.
    /// </summary>
    private MinHeap Queue
    {
        get;
        set;
    }

    /// <summary>
    /// A previous list, containing a list of strings.
    /// </summary>
    private Dictionary<string, string> Previous
    {
        get;
        set;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Dijkstra"/> class.
    /// </summary>
    public Dijkstra()
    {
        // A list of graph nodes.
        this.Graph = new List<GraphNode>();

        // Set Queue to null.
        this.Queue = null;

        // A list of strings.
        this.Previous = new Dictionary<string, string>();

        SetGraph();
    }

    /// <summary>
    /// Method to set the graph of the class to the given graph.
    /// </summary>
    public void SetGraph()
    {
        List<string> listOfNodes = new List<string>() {"alk", "haa", "ams", "lei", "dha", "del", "rot", "utr", "dor", "zie", "mid", "ter", "goe", "roo", "bre", "til",
         "ein", "ven", "roe", "maa", "hrl", "bos", "nij", "arn", "ame", "alm", "lel", "ens", "alo", "ape", "zwo", "hoo", "emm", "ass", "gro", "hee", "sne", "lee"};

        //DE lijst
        List<GraphNode> graphAslist = MakeListOfGraphNodes(listOfNodes);

        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "alk", "haa", 35.7f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "alk", "ams", 42.5f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "alk", "sne", 95.6f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "haa", "ams", 19.5f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "haa", "lei", 35.7f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ams", "alm", 33.5f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ams", "utr", 43.5f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ams", "lei", 46.9f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "lei", "utr", 58.8f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "lei", "dha", 35.7f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "dha", "utr", 68.5f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "dha", "del", 11.6f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "del", "rot", 15.6f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "rot", "utr", 61.8f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "rot", "dor", 25.5f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "rot", "zie", 66.2f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "utr", "dor", 65.8f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "utr", "nij", 84.6f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "utr", "ame", 23.5f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "utr", "alm", 41.3f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "dor", "bos", 65.8f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "dor", "bre", 35.6f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "dor", "roo", 44.7f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "zie", "roo", 59.4f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "zie", "mid", 42.3f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "zie", "goe", 22.9f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "mid", "goe", 25.8f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "mid", "ter", 32.8f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ter", "goe", 35.4f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "roo", "bre", 24.6f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "bre", "til", 28.0f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "til", "bos", 24.1f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "til", "ein", 34.7f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ein", "bos", 32.8f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ein", "nij", 61.3f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ein", "ven", 58.6f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ein", "roe", 51.2f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ven", "nij", 62.4f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ven", "roe", 29.3f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "roe", "maa", 48.3f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "roe", "hrl", 40.9f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "maa", "hrl", 24.9f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "alk", "haa", 35.7f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "bos", "nij", 48.3f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "bos", "utr", 56.9f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "nij", "arn", 23.1f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "arn", "ame", 51.8f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "arn", "ens", 95.7f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "arn", "ape", 31.7f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ame", "alm", 41.5f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "alm", "lel", 30.5f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "alm", "ape", 35.7f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "lel", "zwo", 49.6f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ape", "zwo", 39.0f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ape", "alo", 60.3f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ens", "alo", 29.4f);
        //graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ens", "hoo", 74.3f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "alo", "zwo", 52.3f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "alo", "hoo", 51.6f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "zwo", "hoo", 53.5f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "zwo", "hee", 62.9f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "hoo", "ass", 34.3f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "hoo", "emm", 33.0f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "hoo", "hee", 35.7f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "emm", "ass", 38.9f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "ass", "gro", 32.2f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "gro", "hee", 60.0f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "gro", "lee", 60.0f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "hee", "sne", 25.8f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "hee", "lee", 31.2f);
        graphAslist = AddConnectionsBetweenGraphNodes(graphAslist, "sne", "lee", 22.0f);

        Graph = graphAslist;
    }

    private static List<GraphNode> MakeListOfGraphNodes(List<string> graphNodes)
    {
        if(graphNodes.Count > 0)
        {
            List<GraphNode> listOfEmptyGraphNodes = new List<GraphNode>();

            foreach (string node in graphNodes)
            {
                listOfEmptyGraphNodes.Add(new GraphNode(node, new List<Vertex>()));
            }

            return listOfEmptyGraphNodes;
        }
        return null;
    }

    private static List<GraphNode> AddConnectionsBetweenGraphNodes(List<GraphNode> inputList, string firstLocation, string secondlocation, float distance)
    {
        foreach(GraphNode node in inputList)
        {
            if(string.Equals(node.Name, firstLocation))
            {
                node.Vertices.Add(new Vertex(secondlocation, distance));
            }
            if(string.Equals(node.Name, secondlocation))
            {
                node.Vertices.Add(new Vertex(firstLocation, distance));
            }
        }

        return inputList;
    }

    /// <summary>
    /// Method to get the shortest path between two nodes.
    /// </summary>
    /// <param name="source">The source node as a string.</param>
    /// <param name="target">The target node as a string.</param>
    /// <returns>Returns the shortest path between the two given nodes.</returns>
    public List<string> GetPath(string source, string target)
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
            return new List<string>();
        }

        // Create a new MinHeap.
        this.Queue = new MinHeap();

        // Add the source at the first place.
        this.Queue.Add(source, 0);

        // Set the value of source in this.Previous to null.
        this.Previous[source] = null;

        // Loop through all nodes
        string u;
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
                List<string> path = new List<string>();

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
            if (this.Queue.GetDistance(u).Equals(float.PositiveInfinity))
            {
                // Return an empty list when the remaining vertices are inaccessible from source.
                return new List<string>();
            }

            // Initialize a new float with the distance of u.
            float uDistance = this.Queue.GetDistance(u);

            // Find the right graphNode.
            GraphNode foundGraphNode = this.Graph.Find(n => n.Name == u);

            // Break if the graphNode has not been found.
            if (foundGraphNode == null) break;

            // Loop through the graphNode vertices.
            foreach (Vertex vertex in foundGraphNode.Vertices)
            {
                // Save the distance to the vertex.
                float nDistance = this.Queue.GetDistance(vertex.Name);

                // Save the distance to the vertex + the vertex distance cost.
                float aDistance = uDistance + vertex.Cost;

                // Check if the distance of the vertex + the vertex distance cost is less
                // and the distance of the vertex.
                if(aDistance < nDistance)
                {
                    // Update the vertex.
                    this.Queue.Update(vertex.Name, aDistance);

                    // Store u as the value of the vertex.
                    this.Previous[vertex.Name] = u;
                }
            }
        }
        while (!string.IsNullOrEmpty(u));

        // Return an empty list when a path has not been found.
        return new List<string>();
    }

    /// <summary>
    /// Method to get the length of a given path.
    /// </summary>
    /// <param name="path">A list of strings.</param>
    /// <returns>Returns the length of the path as a float.</returns>
    public float GetPathLength(List<string> path)
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
            string start = path[i];
            string next = path[i + 1];

            // Increase the total length with the length between the two nodes.
            GraphNode gnode = this.Graph.Find(g => g.Name == start);
            totalLength += gnode.Vertices.Find(v => v.Name == next).Cost;
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
        GraphNode graphNode = null;
        List<string> returnedList = new List<string>();

        // Get the index of the nodename in this.Graph.
        GraphNode foundNode = this.Graph.Find(n => n.Name == nodeName);

        // Get the index of the nodename in this.Graph
        foreach (GraphNode node in this.Graph)
        {
            if (string.Equals(nodeName, node.Name))
            {
                graphNode = node;
            }
        }

        if (graphNode != null)
        {
            // Create an empty list to contain the names of neighbour nodes.
            returnedList = new List<string>();

            // Loop through all vertices.
            //foreach (Vertex v in (ArrayList)this.Graph[indexOfNode])
            foreach (Vertex v in foundNode.Vertices)
            {
                // Add the name of the vertex to returnedList/
                returnedList.Add(v.Name);
            }

            // Return the list of neighbour nodes.
            return returnedList;
        }
      

        // Return the list of neighbour nodes
        return returnedList;
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
    private string Min
    {
        get;
        set;
    }

    /// <summary>
    /// A list of roots, stored as strings.
    /// </summary>
    private List<string> Roots
    {
        get;
        set;
    }

    /// <summary>
    /// A list of nodes, stored as Node objects.
    /// </summary>
    private List<Node> Nodes
    {
        get;
        set;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MinHeap"/> class.
    /// </summary>
    public MinHeap()
    {
        this.Min = null;
        this.Roots = new List<string>();
        this.Nodes = new List<Node>();
    }

    /// <summary>
    /// Method to shift a node off the heap.
    /// </summary>
    /// <returns>Returns the string of the shifted node.</returns>
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

        // Loop through all node string in this.Roots to find the lowest distance
        foreach (string node in this.Roots)
        {
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
    private void Consolidate()
    {
        // Consolidate
        ArrayList depths = new ArrayList(7);

        for (int x = 0; x < 7; x++)
        {
            depths[x] = new ArrayList();
        }
        int maxDepth = depths.Count - 1;

        // Populate depths array
        foreach (string node in this.Roots)
        {
            int depth = this.Nodes.Find(n => n.NodeName == node).Depth;

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
                int pos;

                Node firstNode = this.Nodes.Find(n => n.NodeName == first);
                Node secondNode = this.Nodes.Find(n => n.NodeName == second);

                if (firstNode.Distance < secondNode.Distance)
                {
                    firstNode.Depth = newDepth;
                    firstNode.Children.Add(secondNode.NodeName);
                    secondNode.Parent = firstNode.NodeName;

                    if (newDepth <= maxDepth)
                    {
                        ((ArrayList)depths[newDepth]).Add(first);
                    }

                    // Find position in roots where adopted node is.
                    pos = this.Roots.IndexOf(second);
                } else {
                    secondNode.Depth = newDepth;
                    secondNode.Children.Add(firstNode.NodeName);
                    firstNode.Parent = secondNode.NodeName;

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
        // Create a new node with the given parameters.
        Node newNode = new Node(node, distance);

        // Find a node with the given nodename.
        Node foundNode = this.Nodes.Find(n => n.NodeName == node);

        // Add the node if the node with the given name does not exist, else replace this node.
        if (foundNode == null)
        {
            this.Nodes.Add(newNode);
        }
        else
        {
            foundNode = newNode;
        }

        // Is it the minimum?
        if (this.Min == null)
        {
            this.Min = node;
        }
        else
        {
            foundNode = this.Nodes.Find(n => n.NodeName == node);

            // Add the node
            if (foundNode != null)
            {
                if (distance < foundNode.Distance)
                {
                    this.Min = foundNode.NodeName;
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
    private void Remove(string node)
    {
        Node foundNode = this.Nodes.Find(n => n.NodeName == node);

        // Return when the node has not been found.
        if (foundNode == null) return;

        // Move children to be children of the parent.
        foreach (string child in foundNode.Children)
        {
            Node foundChildNode = this.Nodes.Find(n => n.NodeName == child);

            if (foundChildNode.Parent != null)
            {
                foundChildNode.Parent = foundNode.Parent;
            }

            if (foundChildNode.Parent == null)
            {
                this.Roots.Add(foundChildNode.NodeName);
            }
        }

        string parent = foundNode.Parent;

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
                Node foundNode2 = this.Nodes.Find(n => n.NodeName == node);

                if (foundNode2 != null)
                {
                    foundNode2.Depth--;
                    parent = foundNode2.Parent;
                }
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
        Node foundNode = this.Nodes.Find(n => n.NodeName == node);

        if (foundNode != null)
        {
            return foundNode.Distance;
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
    /// Gets the name of the node.
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the list of vertices of the node.
    /// </summary>
    public List<Vertex> Vertices
    {
        get;
        private set;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GraphNode"/> class.
    /// </summary>
    /// <param name="name">The name of the graph node.</param>
    /// <param name="vertices">The list of vertices of the graph node.</param>
    public GraphNode(string name, List<Vertex> vertices)
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
    /// Gets the name of the vertex.
    /// </summary>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the cost (length) of the vertex.
    /// </summary>
    public float Cost
    {
        get;
        private set;
    }

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
    /// Gets or sets the depth as an int.
    /// </summary>
    public int Depth
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the parent node name.
    /// </summary>
    public string Parent
    {
        get;
        set;
    }

    /// <summary>
    /// Gets the node name.
    /// </summary>
    public string NodeName
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the distance as a float.
    /// </summary>
    public float Distance
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the children node names.
    /// </summary>
    public List<string> Children
    {
        get;
        private set;
    }

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
        this.Children = new List<string>();
    }
}
