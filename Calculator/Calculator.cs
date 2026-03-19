using System.Linq.Expressions;
using Calculator.AST;

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
        return Checker.Check(expression, _functions);
    }
    public bool CheckFunction(string expression)
    {
        if (!expression.Contains('='))
        {
            return false;
        }
        bool check = Checker.CheckForFunctionDefinition(expression, _functions, out Function temporaryFunction);
        if (check) AddFunction(temporaryFunction);
        else Console.WriteLine("function not added");
        return check;
    }

    public double CalculateFunction(string expression, double[] arguments)
    {
        var calculableForm = Function.GetCalculatableForm(expression, arguments);
        return Calculate(calculableForm);
    }

    public void CalculateWithAST(string expression)
    {
        if (!CheckExpression(expression))
        {
            Console.WriteLine("You are invalid");
            return;
        }
        var tokens = Tokenizer.Tokenize(expression);
        // Console.WriteLine("Start tokens");tokens.ShowQueue();Console.WriteLine("End tokens");
        var postfix = ToPostFixConvertor.ToPostFixConvert(tokens, _functions);
        
        var tokensAST = Tokenizer.Tokenize(expression);
        var postfixAST = ToPostFixConvertor.ToPostFixConvert(tokensAST, _functions);

        // Console.WriteLine("Start postfix");postfix.ShowQueue();Console.WriteLine("End postfix");
        Console.WriteLine(CalculatePostFix(postfix));
        ASTBuilder.BuildAST(postfixAST, _functions);
    }
    public double Calculate(string expression)
    {
        if (!CheckExpression(expression))
        {
            Console.WriteLine("You are invalid");
            return 0;
        }
        var tokens = Tokenizer.Tokenize(expression);
        // Console.WriteLine("Start tokens");tokens.ShowQueue();Console.WriteLine("End tokens");
        var postfix = ToPostFixConvertor.ToPostFixConvert(tokens, _functions);
        // Console.WriteLine("Start postfix");postfix.ShowQueue();Console.WriteLine("End postfix");
        return CalculatePostFix(postfix);
    }

    public void CheckFunctionNew(string expression)
    {
        Console.WriteLine(Checker.CheckFunctionValidity(expression, 3, _functions));
    }
    public void ShowFunctions()
    {
        _functions.ShowList();
    }
    public double CalculatePostFix(OwnQueue<string> postFix)
    {
        OwnStack<double> stack = new OwnStack<double>(postFix.Size);
        // Console.WriteLine("Start Postfix");postFix.ShowQueue();Console.WriteLine("End postfix");

        while (postFix.Size != 0)
        {
            string value = postFix.Pop();
            if (double.TryParse(value.Replace(".",","), out double result))
            {
                stack.Push(result);
                continue;
            }

            Function function = _functions.GetElement(function => function.Symbol, value);
            double[] arguments = new double[function.NumberOfArguments];
            for (int index = 0; index < function.NumberOfArguments; index++)
            {
                arguments[function.NumberOfArguments - index - 1] = stack.Pull();
                // Console.WriteLine($" index of 1 - {function.NumberOfArguments - index - 1}");
            }
            // Console.WriteLine($"arg 0 - {arguments[0]}");
            stack.Push(function.Calculate(arguments));
            
        }

        double var = stack.Pull();
        if (stack.StackSize != 0)
        {
            // stack.ShowStack();
            Console.WriteLine("Invalid Expression");
            throw new Exception("Something");
        }
        return var;
    }
}