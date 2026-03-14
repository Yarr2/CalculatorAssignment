using System.Linq.Expressions;

namespace Calculator;

public class Calculator
{
    private OwnList<Function> _functions;
    
    public Calculator(OwnList<Function> functions)
    {
        _functions = functions;
    }

    public void AddFunction(Function function)
    {
        _functions.Append(function);
    }

    public bool CheckExpression(string expression)
    {
        if (expression.Contains("=")) return false;
        return Checker.Check(Tokenizer.Tokenize(expression), _functions);
    }
    public bool CheckFunction(string expression)
    {
        bool check = Checker.CheckForFunctionDefinition(expression, _functions, out Function temporaryFunction);
        if (check) AddFunction(temporaryFunction);
        return check;
    }

    public double CalculateFunction(string expression, double[] arguments)
    {
        var calculableForm = Function.GetCalculatableForm(expression, arguments);
        var tokens = Tokenizer.Tokenize(calculableForm);
        var postfix = ToPostFixConvertor.ToPostFixConvert(tokens, _functions);
        return CalculatePostFix(postfix);
    }

    public double Calculate(string expression)
    {
        var tokens = Tokenizer.Tokenize(expression);
        var postfix = ToPostFixConvertor.ToPostFixConvert(tokens, _functions);
        return CalculatePostFix(postfix);
    }

    public void ShowFunctions()
    {
        _functions.ShowList();
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

            Function function = _functions.GetElement(function => function.Symbol, value);
            double[] arguments = new double[function.NumberOfArguments];
            for (int index = 0; index < function.NumberOfArguments; index++)
            {
                arguments[index] = stack.Pull();
            }
            stack.Push(function.Calculate(arguments));
            
        }

        double var = stack.Pull();
        return var;
    }
}