namespace Calculator;

public class Calculator
{
    private List<Function> allFunc;
    private List<BinaryFunction> _binaryFunctions;
    public void Calculate()
    {
        string expression = Console.ReadLine();
        expression.TransformToBinaryOpperations();
        expression.TransformToPostFix();
        expression.Calculate();


    }
}