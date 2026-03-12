namespace Calculator;

public class Function
{
    private int _numberOfArguments;
    private string _name;
    private string _implementation;

    public string Implementation => _implementation;
    public string Symbol()
    {
        return _name;
    }


    public Function(string name,string implementation, int numberOfArguments)
    {
        _name = name;
        _implementation = implementation;
        _numberOfArguments = numberOfArguments;
    }
    public string GetCalculatableForm(double[] arguments,OwnList<BinaryOperation> operations)
    {
        if (arguments.Length != _numberOfArguments)
        {
            throw new Exception($"This function takes {_numberOfArguments} instead of {arguments.Length} given");
        }
        string[] tokens = Tokenizer.Tokenize(_implementation, operations).ToArray();
        int index = 2;
        string result = "";
        while (tokens[index] != ")")
        {
            for (int secondaryIndex = tokens.IndexOf("="); secondaryIndex < tokens.Length; secondaryIndex++)
            {
                if (tokens[secondaryIndex] == tokens[index])
                {
                    tokens[secondaryIndex] = arguments[index - 2].ToString();
                }
            }

            index++;
        }

        for (int secondaryIndex = tokens.IndexOf("="); secondaryIndex < tokens.Length; secondaryIndex++)
        {
            result += tokens[secondaryIndex];
        }

        return result;
    }

    public double Calculate(double[] arguments, OwnList<Function> functions)
    {
        return 0;
    }
}

// f(x,y,xy) = x^2 + 2xy 