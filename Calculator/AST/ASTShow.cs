namespace Calculator.AST;

public class ASTShow
{
    public static void Visualise(Node node, string indent = "", bool isLast = true)
    {
        string marker = isLast ? "└── " : "├── ";
        
        Console.Write(indent);
        Console.Write(marker);
        Console.WriteLine(node.Name);

        indent += isLast ? "    " : "│   ";

        for (int i = 0; i < node.Arguments.Size; i++)
        {
            Visualise(node.Arguments.GetElement(i), indent, i == node.Arguments.Size - 1);
        }
    }

}