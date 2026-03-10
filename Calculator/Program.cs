namespace Calculator;

class Program
{
    static void Main(string[] args)
    {
        List<BinaryOperation> binOperations = new List<BinaryOperation>()
        {
            new BinaryOperation("+", 0, Associativity.Right, args => args[0] + args[1]),
            new BinaryOperation("-", 0, Associativity.Right, args => args[0] + args[1]),
            new BinaryOperation("*", 0, Associativity.Right, args => args[0] + args[1]),
            new BinaryOperation("/", 0, Associativity.Right, args => args[0] + args[1]),
            new BinaryOperation("^", 0, Associativity.Right, args => args[0] + args[1]),
            new BinaryOperation("(", 0, Associativity.Right, args => args[0] + args[1]),
            new BinaryOperation(")", 0, Associativity.Right, args => args[0] + args[1]),
            };
        
        Tokenizer tokenizer = new Tokenizer();
        string text = Console.ReadLine();
        Tokenizer.Tokenize(text,binOperations).ShowQueue();
    }
}