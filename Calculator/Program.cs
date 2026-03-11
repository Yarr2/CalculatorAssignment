namespace Calculator;

class Program
{
    static void Main(string[] args)
    {
        OwnList<BinaryOperation> binOperations = new OwnList<BinaryOperation>();
        binOperations.Append(new BinaryOperation("+", 2, Associativity.Left, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation("-", 2, Associativity.Left, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation("*", 3, Associativity.Left, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation("/", 3, Associativity.Left, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation("^", 4, Associativity.Right, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation("(", 0, Associativity.Left, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation(")", 0, Associativity.Left, args => args[0] + args[1]));
        
        string text = Console.ReadLine();
        var queue = Tokenizer.Tokenize(text, binOperations);
        ToPostFixConvertor.ToPostFixConvert(queue, binOperations).ShowQueue();
    }
}