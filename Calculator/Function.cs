namespace Calculator;

public enum Associativity
{
    Left,Right
}

public enum TypeOfFunction
{
    Function,Operation
}
public class Function(
    string name,
    int numberOfArguments,
    int preference,
    Associativity associativity,
    Func<double[], double> function,
    TypeOfFunction type)
{
    private Func<double[],double> _calculate = function;
    public readonly int Preference = preference;
    public readonly Associativity Assosiativity = associativity;
    public readonly int NumberOfArguments = numberOfArguments;
    private string name = name;
    public readonly TypeOfFunction TypeOfFunction = type;
    public string Symbol
    {
        get => name;
    }

    public double Calculate(double[] arguments)
    {
        return _calculate(arguments);
    }

    public static string GetCalculatableForm(string implementation , double[] arguments)
    {
        string[] tokens = Tokenizer.Tokenize(implementation, skippableSymbols:new char[1]{' '}).ToArray();
        bool IsVariable = false;
        if (tokens[1] == "=") IsVariable = true;
        int index = 2;
        string result = "";
        while (tokens[index] != ")" && !IsVariable)
        {
            if (tokens[index] == ",")
            {
                index++;
                continue;
            }
            for (int secondaryIndex = tokens.IndexOf("="); secondaryIndex < tokens.Length; secondaryIndex++)
            {
                if (tokens[secondaryIndex] == tokens[index])
                {
                    tokens[secondaryIndex] = arguments[(index - 2)/2].ToString();
                }
            }

            index++;
        }

        for (int secondaryIndex = tokens.IndexOf("=") + 1; secondaryIndex < tokens.Length; secondaryIndex++)
        {
            result += tokens[secondaryIndex];
        }

        return result;
    }
    public static bool Comparator(Function left, Function right)
    {
        if (right == default(Function)) return false;
        return (left.Preference < right.Preference ||
                (left.Preference == right.Preference &&
                 left.Assosiativity == Associativity.Left));
    }

    public override string ToString()
    {
        return $"Function {Symbol}, {numberOfArguments}";
    }
}

// f(x,y,xy) = x^2 + 2xy 