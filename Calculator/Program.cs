namespace Calculator;

class Program
{
    static void Main(string[] args)
    {
        OwnList<BinaryOperation> binOperations = new OwnList<BinaryOperation>();
        binOperations.Append(new BinaryOperation("+", 0, Associativity.Right, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation("-", 0, Associativity.Right, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation("*", 0, Associativity.Right, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation("/", 0, Associativity.Right, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation("^", 0, Associativity.Right, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation("(", 0, Associativity.Right, args => args[0] + args[1]));
        binOperations.Append(new BinaryOperation(")", 0, Associativity.Right, args => args[0] + args[1]));
        
        Tokenizer tokenizer = new Tokenizer();
        string text = Console.ReadLine();
        Tokenizer.Tokenize(text,binOperations).ShowQueue();
    }
}