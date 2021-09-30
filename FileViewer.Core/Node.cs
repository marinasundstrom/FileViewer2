namespace FileViewer;

public class Node
{
    private List<Node>? childNodes;
    internal NodeTree? tree;

    public Node(string name, bool isFile)
    {
        Name = name;
        IsFile = isFile;
    }

    public string Name { get; }

    public bool IsFile { get; }

    public bool IsDirectory => !IsFile;

    public IEnumerable<Node> ChildNodes => childNodes?.ToArray() ?? Array.Empty<Node>();

    public Node? Parent { get; internal set; }

    public NodeTree? Tree
    {
        get
        {
            if (Parent is null)
                return tree;

            return Parent.Tree;
        }
    }

    public Node? GetChildNode(string name) => ChildNodes.FirstOrDefault(x => x.Name == name);


    public Node CreateFile(string name)
    {
        var node = new Node(name, true);
        AttachChildNode(node);
        return node;
    }

    public Node CreateDirectory(string name)
    {
        var node = new Node(name, false);
        AttachChildNode(node);
        return node;
    }

    public void Detach()
    {
        if (Parent is null) throw new Exception("Cannot detach a node without a Parent");

        this.Parent?.DetachChildNode(this);
    }

    public override string ToString() => $"{Name}{(IsFile ? " [File]" : string.Empty)}";

    internal void AttachChildNode(Node node)
    {
        if (IsFile) throw new Exception("Cannot add Child Nodes to files");

        if (childNodes is null)
        {
            childNodes = new List<Node>();
        }

        if(childNodes.Any(x => x.Name == node.Name))
        {
            throw new Exception($"The parent node already has a child named {node.Name}");
        }

        node.Parent = this;

        childNodes.Add(node);

        Tree?.NotifyNodeAdded(this, node);
    }

    internal void DetachChildNode(Node node)
    {
        node.Parent = null;

        childNodes?.Remove(node);

        Tree?.NotifyNodeRemoved(this, node);
    }

    private Node GetRootNode()
    {
        if (Parent is null)
            return this;

        return Parent.GetRootNode();
    }
}