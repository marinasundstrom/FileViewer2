using System;
namespace FileViewer;

public class NodeTree
{
    public NodeTree(Node root)
    {
        if (root.Parent is not null)
        {
            throw new ArgumentException("Node must not have a Parent", nameof(root));
        }

        Root = root;
        root.tree = this;
    }

    public Node Root { get; }

    public event EventHandler<ChildNodeEventArgs>? NodeAdded;

    public event EventHandler<ChildNodeEventArgs>? NodeRemoved;

    internal void NotifyNodeAdded(Node parentNode, Node node)
    {
        NodeAdded?.Invoke(this, new ChildNodeEventArgs(parentNode, node));
    }

    internal void NotifyNodeRemoved(Node parentNode, Node node)
    {
        NodeRemoved?.Invoke(this, new ChildNodeEventArgs(parentNode, node));
    }
}

public class ChildNodeEventArgs : EventArgs
{
    public Node ParentNode { get; }

    public Node Node { get; }

    public ChildNodeEventArgs(Node parentNode, Node node)
    {
        ParentNode = parentNode;
        Node = node;
    }
}
