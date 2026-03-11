namespace Calculator;

public class Calculator
{
    private OwnList<BinaryOperation> _operations;

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
            _operations.
            
        }
    }
}