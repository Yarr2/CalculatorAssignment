using System.ComponentModel.DataAnnotations;

namespace Calculator;

class Program
{
    static void Main(string[] args) // "12 3 +"
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
        binOperations.Append(new BinaryOperation("abs",0,Associativity.Left,args => Math.Abs(args[0])));
        
        Calculator calculator = new Calculator(binOperations);
        string text = Console.ReadLine();
        var queue = Tokenizer.Tokenize(text);
        var postfix = ToPostFixConvertor.ToPostFixConvert(queue, binOperations);
        Console.WriteLine("smth");
        postfix.ShowQueue();
        Console.WriteLine("smth");
        calculator.CalculatePostFix(postfix);
    }
}

// queue - [smth smth func ( 1 2 4 ) smth smth smth] -> 
// queue - [smth smth 1 + 2 + 4 smth smth smth]