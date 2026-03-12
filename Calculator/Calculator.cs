namespace Calculator;

public class Calculator
{
    private OwnList<BinaryOperation> _operations;
    private OwnList<Function> _functions;

    public Calculator(OwnList<BinaryOperation> operations)
    {
        _operations = operations;
    }
    public double CalculatePostFix(OwnQueue<string> postFix)
    {
        OwnStack<double> stack = new OwnStack<double>(postFix.Size);
        while (postFix.Size != 0)
        {
            string value = postFix.Pop();
            if (double.TryParse(value, out double result))
            {
                stack.Push(result);
                continue;
            }

            BinaryOperation operation = _operations.GetElement(operation => operation.Symbol, value);
            stack.Push(operation._calculate(new double[] { stack.Pull(), stack.Pull() }));
            
        }

        double var = stack.Pull();
        Console.WriteLine(var);
        return var;
    }
}