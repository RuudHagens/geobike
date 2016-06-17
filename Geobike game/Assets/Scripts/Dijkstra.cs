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
    /// <param name="graph">The graph to set.</param>
    public void SetGraph()
    {
        //DE lijst
        List<GraphNode> graphAslist = new List<GraphNode>();

        //DE lijst binnen de lijst
        List<Vertex> vertexForList = new List<Vertex>();

        vertexForList.Add(new Vertex("haa", 35.7f));
        vertexForList.Add(new Vertex("ams", 42.5f));
        vertexForList.Add(new Vertex("sne", 95.6f));
        graphAslist.Add(new GraphNode("alk", new List<Vertex>(vertexForList)));
        vertexForList.Clear();


        vertexForList.Add(new Vertex("alk", 35.7f));
        vertexForList.Add(new Vertex("ams", 19.5f));
        vertexForList.Add(new Vertex("lei", 33.7f));
        graphAslist.Add(new GraphNode("haa", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("alk", 42.5f));
        vertexForList.Add(new Vertex("haa", 19.5f));
        vertexForList.Add(new Vertex("alm", 33.5f));
        vertexForList.Add(new Vertex("utr", 43.5f));
        vertexForList.Add(new Vertex("lei", 46.9f));
        graphAslist.Add(new GraphNode("ams", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("haa", 33.7f));
        vertexForList.Add(new Vertex("ams", 46.9f));
        vertexForList.Add(new Vertex("utr", 58.8f));
        vertexForList.Add(new Vertex("dha", 24.1f));
        graphAslist.Add(new GraphNode("lei", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("lei", 24.1f));
        vertexForList.Add(new Vertex("utr", 68.5f));
        vertexForList.Add(new Vertex("del", 11.6f));
        graphAslist.Add(new GraphNode("dha", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("dha", 11.6f));
        vertexForList.Add(new Vertex("rot", 15.6f));
        graphAslist.Add(new GraphNode("del", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("del", 15.6f));
        vertexForList.Add(new Vertex("utr", 61.8f));
        vertexForList.Add(new Vertex("dor", 25.5f));
        vertexForList.Add(new Vertex("zie", 66.2f));
        graphAslist.Add(new GraphNode("rot", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ams", 43.5f));
        vertexForList.Add(new Vertex("lei", 58.8f));
        vertexForList.Add(new Vertex("dha", 68.5f));
        vertexForList.Add(new Vertex("rot", 61.8f));
        vertexForList.Add(new Vertex("dor", 65.8f));
        vertexForList.Add(new Vertex("bos", 56.9f));
        vertexForList.Add(new Vertex("nij", 84.6f));
        vertexForList.Add(new Vertex("ame", 23.5f));
        vertexForList.Add(new Vertex("alm", 41.3f));
        graphAslist.Add(new GraphNode("utr", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("rot", 25.5f));
        vertexForList.Add(new Vertex("utr", 65.8f));
        vertexForList.Add(new Vertex("bos", 62.8f));
        vertexForList.Add(new Vertex("bre", 35.6f));
        vertexForList.Add(new Vertex("roo", 44.7f));
        graphAslist.Add(new GraphNode("dor", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("rot", 66.2f));
        vertexForList.Add(new Vertex("roo", 59.4f));
        vertexForList.Add(new Vertex("mid", 42.3f));
        vertexForList.Add(new Vertex("goe", 22.9f));
        graphAslist.Add(new GraphNode("zie", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("zie", 42.3f));
        vertexForList.Add(new Vertex("goe", 25.8f));
        vertexForList.Add(new Vertex("ter", 32.8f));
        graphAslist.Add(new GraphNode("mid", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("mid", 32.8f));
        vertexForList.Add(new Vertex("goe", 35.4f));
        graphAslist.Add(new GraphNode("ter", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ter", 35.4f));
        vertexForList.Add(new Vertex("mid", 25.8f));
        vertexForList.Add(new Vertex("zie", 22.9f));
        graphAslist.Add(new GraphNode("goe", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("zie", 59.4f));
        vertexForList.Add(new Vertex("dor", 44.7f));
        vertexForList.Add(new Vertex("bre", 24.6f));
        graphAslist.Add(new GraphNode("roo", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("roo", 24.6f));
        vertexForList.Add(new Vertex("dor", 35.6f));
        vertexForList.Add(new Vertex("til", 28.0f));
        graphAslist.Add(new GraphNode("bre", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("bre", 28.0f));
        vertexForList.Add(new Vertex("bos", 24.1f));
        vertexForList.Add(new Vertex("ein", 34.7f));
        graphAslist.Add(new GraphNode("til", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("til", 34.7f));
        vertexForList.Add(new Vertex("bos", 32.8f));
        vertexForList.Add(new Vertex("nij", 61.3f));
        vertexForList.Add(new Vertex("ven", 58.6f));
        vertexForList.Add(new Vertex("roe", 51.2f));
        graphAslist.Add(new GraphNode("ein", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ein", 58.6f));
        vertexForList.Add(new Vertex("nij", 62.4f));
        vertexForList.Add(new Vertex("roe", 29.3f));
        graphAslist.Add(new GraphNode("ven", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ven", 29.3f));
        vertexForList.Add(new Vertex("ein", 31.2f));
        vertexForList.Add(new Vertex("maa", 48.3f));
        vertexForList.Add(new Vertex("hrl", 40.9f));
        graphAslist.Add(new GraphNode("roe", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("roe", 48.3f));
        vertexForList.Add(new Vertex("hrl", 24.9f));
        graphAslist.Add(new GraphNode("maa", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("maa", 24.9f));
        vertexForList.Add(new Vertex("roe", 40.9f));
        graphAslist.Add(new GraphNode("hrl", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("til", 24.1f));
        vertexForList.Add(new Vertex("ein", 32.8f));
        vertexForList.Add(new Vertex("nij", 48.3f));
        vertexForList.Add(new Vertex("utr", 56.9f));
        vertexForList.Add(new Vertex("dor", 62.8f));
        graphAslist.Add(new GraphNode("bos", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ven", 62.4f));
        vertexForList.Add(new Vertex("ein", 61.3f));
        vertexForList.Add(new Vertex("bos", 48.3f));
        vertexForList.Add(new Vertex("utr", 84.6f));
        vertexForList.Add(new Vertex("arn", 23.1f));
        graphAslist.Add(new GraphNode("nij", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("nij", 23.1f));
        vertexForList.Add(new Vertex("ame", 51.8f));
        vertexForList.Add(new Vertex("ens", 95.7f));
        vertexForList.Add(new Vertex("ape", 31.7f));
        graphAslist.Add(new GraphNode("arn", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("arn", 51.8f));
        vertexForList.Add(new Vertex("utr", 23.4f));
        vertexForList.Add(new Vertex("alm", 41.5f));
        graphAslist.Add(new GraphNode("ame", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ame", 41.5f));
        vertexForList.Add(new Vertex("utr", 41.3f));
        vertexForList.Add(new Vertex("ams", 33.5f));
        vertexForList.Add(new Vertex("lel", 30.5f));
        vertexForList.Add(new Vertex("ape", 74.1f));
        graphAslist.Add(new GraphNode("alm", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("alm", 30.5f));
        vertexForList.Add(new Vertex("zwo", 49.6f));
        graphAslist.Add(new GraphNode("lel", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("arn", 31.7f));
        vertexForList.Add(new Vertex("alm", 74.1f));
        vertexForList.Add(new Vertex("zwo", 39.0f));
        vertexForList.Add(new Vertex("alo", 60.3f));
        graphAslist.Add(new GraphNode("ape", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("arn", 95.7f));
        vertexForList.Add(new Vertex("alo", 29.4f));
        vertexForList.Add(new Vertex("hoo", 74.3f));
        graphAslist.Add(new GraphNode("ens", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ens", 29.4f));
        vertexForList.Add(new Vertex("ape", 60.3f));
        vertexForList.Add(new Vertex("zwo", 52.3f));
        vertexForList.Add(new Vertex("hoo", 51.6f));
        graphAslist.Add(new GraphNode("alo", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("lel", 49.6f));
        vertexForList.Add(new Vertex("ape", 39.0f));
        vertexForList.Add(new Vertex("alo", 52.3f));
        vertexForList.Add(new Vertex("hoo", 53.5f));
        vertexForList.Add(new Vertex("hee", 62.9f));
        graphAslist.Add(new GraphNode("zwo", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ens", 74.3f));
        vertexForList.Add(new Vertex("alo", 51.6f));
        vertexForList.Add(new Vertex("zwo", 43.5f));
        vertexForList.Add(new Vertex("hee", 63.5f));
        vertexForList.Add(new Vertex("ass", 34.3f));
        vertexForList.Add(new Vertex("emm", 33.0f));
        graphAslist.Add(new GraphNode("hoo", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("hoo", 33.0f));
        vertexForList.Add(new Vertex("ass", 38.9f));
        graphAslist.Add(new GraphNode("emm", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("emm", 38.9f));
        vertexForList.Add(new Vertex("hoo", 34.3f));
        vertexForList.Add(new Vertex("gro", 32.2f));
        graphAslist.Add(new GraphNode("ass", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("ass", 32.2f));
        vertexForList.Add(new Vertex("hee", 60.0f));
        vertexForList.Add(new Vertex("lee", 60.0f));
        graphAslist.Add(new GraphNode("gro", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("zwo", 62.9f));
        vertexForList.Add(new Vertex("hoo", 63.5f));
        vertexForList.Add(new Vertex("gro", 60.0f));
        vertexForList.Add(new Vertex("lee", 31.2f));
        vertexForList.Add(new Vertex("sne", 25.8f));
        graphAslist.Add(new GraphNode("hee", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("hee", 25.8f));
        vertexForList.Add(new Vertex("lee", 22.0f));
        vertexForList.Add(new Vertex("alk", 95.6f));
        graphAslist.Add(new GraphNode("sne", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        vertexForList.Add(new Vertex("sne", 22.0f));
        vertexForList.Add(new Vertex("hee", 31.2f));
        vertexForList.Add(new Vertex("gro", 60.0f));
        graphAslist.Add(new GraphNode("lee", new List<Vertex>(vertexForList)));
        vertexForList.Clear();

        Graph = graphAslist;
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
            if (this.Queue.GetDistance(u) == float.PositiveInfinity)
            {
                // Return an empty list when the remaining vertices are inaccessible from source.
                return new List<string>();
            }

            // Initialize a new float with the distance of u.
            float uDistance = this.Queue.GetDistance(u);

            // Find the index of u in this.Graph.
            int indexFoundNode = -1;
            for (int i = 0; i < Graph.Count; i++)
            {
                if (string.Equals(Graph[i].Name, u))
                {
                    indexFoundNode = i;
                }
            }
            if (indexFoundNode == -1)
            {
                break;
            }

            // Get a list of neighbours.
            List<Vertex> neighbours = this.Graph[indexFoundNode].Vertices;
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
        while (!string.IsNullOrEmpty(u));

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
        //int indexOfNode = this.Graph.IndexOf(nodeName);
        int indexOfNode = this.Graph.IndexOf(this.Graph.Find(n => n.Name == nodeName));

        // Get the index of the nodename in this.Graph
        foreach (GraphNode node in Graph)
        {
            if (node.Name.Equals(nodeName))
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
            foreach (Vertex v in this.Graph[indexOfNode].Vertices)
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
        for (int i = 0; i < this.Roots.Count; i++)
        {
            string node = this.Roots[i];
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
                int pos = -1;

                Node firstNode = this.Nodes.Find(n => n.NodeName == first);
                Node secondNode = this.Nodes.Find(n => n.NodeName == second);

                //if (((Node)this.Nodes[this.Nodes.IndexOf(first)]).Distance < ((Node)this.Nodes[this.Nodes.IndexOf(second)]).Distance)
                if (firstNode.Distance < secondNode.Distance)
                {
                    //((Node)this.Nodes[this.Nodes.IndexOf(first)]).Depth = newDepth;
                    //((Node)this.Nodes[this.Nodes.IndexOf(first)]).Children.Add(second);
                    //((Node)this.Nodes[this.Nodes.IndexOf(second)]).Parent = first;
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
                    //((Node)this.Nodes[this.Nodes.IndexOf(second)]).Depth = newDepth;
                    //((Node)this.Nodes[this.Nodes.IndexOf(second)]).Children.Add(first);
                    //((Node)this.Nodes[this.Nodes.IndexOf(first)]).Parent = second;
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
    private void Remove(string node)
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
    public ArrayList Children
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
        this.Children = new ArrayList();
    }
}
