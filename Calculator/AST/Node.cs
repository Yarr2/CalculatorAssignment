namespace Calculator.AST;

public class Node
{
    public string Name { get; private set; }
    private OwnList<Node> _arguments = new OwnList<Node>();
    public int Layer = 0;

    public Node(string name)
    {
        Name = name;
    }

    public OwnList<Node> Arguments => _arguments;

    public void AddArgument(Node node)
    {
        _arguments.Append(node);
    }

    public override string ToString()
    {
        return $"name - {Name}, arguments count - {_arguments.Size},layer - {Layer}";
    }
}