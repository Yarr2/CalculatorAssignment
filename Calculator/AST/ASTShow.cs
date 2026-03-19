namespace Calculator.AST;

public class ASTShow
{
    public static void Visualise(Node node, string indent = "", bool last = true)
    {
        string marker;
        if (last) marker = "└── ";
        else marker = "├── ";
        
        Console.Write(indent);
        Console.Write(marker);
        Console.WriteLine(node.Name);

        if (last) indent += "    ";
        else indent += "│   ";

        for (int index = 0; index < node.Arguments.Size; index++)
        {
            Visualise(node.Arguments.GetElement(index), indent, index == node.Arguments.Size - 1);
        }
    }

}