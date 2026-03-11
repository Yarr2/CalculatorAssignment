using System.ComponentModel.DataAnnotations;

namespace Calculator;

class Program
{
    static void Main(string[] args)
    {
        OwnList<BinaryOperation> binOperations = new OwnList<BinaryOperation>();
        binOperations.Append(new BinaryOperation("+", 2, Associativity.Left, args => args[1] + args[0]));
        binOperations.Append(new BinaryOperation("-", 2, Associativity.Left, args => args[1] - args[0]));
        binOperations.Append(new BinaryOperation("*", 3, Associativity.Left, args => args[1] * args[0]));
        binOperations.Append(new BinaryOperation("/", 3, Associativity.Left, args => args[1] / args[0]));
        binOperations.Append(new BinaryOperation("^", 4, Associativity.Right, args => Math.Pow(args[1],args[0])));
        binOperations.Append(new BinaryOperation("(", 0, Associativity.Left, args => args[1] + args[0]));
        binOperations.Append(new BinaryOperation(")", 0, Associativity.Left, args => args[1] + args[0]));
        binOperations.Append(new BinaryOperation("max", 9, Associativity.Left, args => Math.Max(args[1],args[0])));
        binOperations.Append(new BinaryOperation("=",0,Associativity.Left,args => args[0]));
        Function function = new Function("f","f(x,y) = x^2 + y - 5",2);
        Console.WriteLine(function.GetCalculatableForm(new double[]{3,2},binOperations));
        
        Calculator calculator = new Calculator(binOperations);
        string text = Console.ReadLine();
        var queue = Tokenizer.Tokenize(text, binOperations);
        var postfix = ToPostFixConvertor.ToPostFixConvert(queue, binOperations);
        Console.WriteLine("smth");
        postfix.ShowQueue();
        Console.WriteLine("smth");
        calculator.CalculatePostFix(postfix);
    }
}