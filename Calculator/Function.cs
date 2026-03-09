namespace Calculator;

public class Function
{
    private int _numberOfArguments;
    private string _name;
    private string _implementation;

    public string Implementation => _implementation;

    public Function(string name,string implementation, int numberOfArguments)
    {
        _name = name;
        _implementation = implementation;
        _numberOfArguments = numberOfArguments;
    }
    public double Calculate(double[] arguments)
    {
        if (arguments.Length != _numberOfArguments)
        {
            throw new Exception($"This function takes {_numberOfArguments} instead of {arguments.Length} given");
        }

        return arguments[0];
    }
}

// f(x,y,xy) = x^2 + 2xy 