namespace Calculator;

public class Function
{
    private int _numberOfArguments;
    private string _name;

    public double Calculate(double[] arguments)
    {
        if (arguments.Length != _numberOfArguments)
        {
            throw new Exception($"This function takes {_numberOfArguments} instead of {arguments.Length} given");
        }

        return arguments[0];
    }
}