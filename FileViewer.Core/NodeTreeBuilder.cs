namespace FileViewer;

public class NodeTreeBuilder
{
    const string rootName = "comics";

    public Node BuildTree(IEnumerable<string> paths)
    {
        Node rootNode = new Node(rootName, false);

        foreach (var path in paths)
        {
            BuildTree(rootNode, path);
        }

        return rootNode;
    }

    private void BuildTree(Node currentNode, string path)
    {
        var pathParts = path
            .TrimStart('/')
            .Split('/');

        var pathPartsQueue = new Queue<string>(pathParts);

        BuildTree(currentNode, pathPartsQueue);
    }

    private void BuildTree(Node currentNode, Queue<string> pathParts)
    {
        var pathPart = pathParts.Dequeue();

        var existingNode = currentNode.GetChildNode(pathPart);

        if (existingNode is null)
        {
            var isFile = !pathParts.Any() && pathPart.IndexOf('.') > 0;

            Node newNode = BuildNode(pathPart, isFile);

            currentNode.AttachChildNode(newNode);

            existingNode = newNode;
        }

        if (pathParts.Count > 0)
        {
            if (existingNode.IsFile)
            {
                throw new Exception();
            }

            BuildTree(existingNode, pathParts);
        }
    }

    private static Node BuildNode(string name, bool isFile)
    {
        Node newNode = new(name, isFile);
        return newNode;
    }
}
