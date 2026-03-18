namespace Calculator.AST;

public class ASTBuilder
{
    public static void BuildAST(OwnQueue<string> postfix, OwnList<Function> functions)
    {
        OwnList<Node> nodes = GetNodes(postfix, functions);
        nodes = GetLevelsForNodes(nodes);
        Console.WriteLine("Start nodes");nodes.ShowList();Console.WriteLine("End nodes");

    }
    public static OwnList<Node> GetNodes(OwnQueue<string> postfix, OwnList<Function> functions)
    {
        OwnStack<Node> nodes = new OwnStack<Node>(postfix.Size);
        OwnList<Node> allNodes = new OwnList<Node>();
        while (postfix.Size > 0)
        {
            string value = postfix.Pop();
            Node node = new Node(value);
            if (double.TryParse(value.Replace(".",","), out double result))
            {
                nodes.Push(node);
                allNodes.Append(node);
                continue;
            }
            Function function = functions.GetElement(function => function.Symbol, value);
            for (int index = 0; index < function.NumberOfArguments; index++)
            {
                Node argumentNode = nodes.Pull();
                node.AddArgument(argumentNode);
            }
            nodes.Push(node);
            allNodes.Append(node);
        }


        return allNodes;
    }

    public static OwnList<Node> GetLevelsForNodes(OwnList<Node> nodes)
    {
        OwnQueue<Node> nodesForBFS = new OwnQueue<Node>(nodes.Size);
        OwnList<Node> layeredNodes = new OwnList<Node>();
        nodesForBFS.Add(nodes.GetElement(nodes.Size - 1));
        while (nodesForBFS.Size > 0)
        {
            Node nextNode = nodesForBFS.Pop();
            for (int index = 0; index < nextNode.Arguments.Size; index++)
            {
                nextNode.Arguments.GetElement(index).Layer = nextNode.Layer + 1;
                nodesForBFS.Add(nextNode.Arguments.GetElement(index));
            }
            layeredNodes.Append(nextNode);
        }

        return layeredNodes;
    }
}



