using System.ComponentModel.DataAnnotations;

namespace Calculator;

class Program
{
    static void Main(string[] args) // "12 3 +" 2^3^2 -> 2 ^ (3 ^ 2) -> 2 ^ 9 -> 512
    {
        OwnList<Function> functions = new OwnList<Function>();
        Calculator calculator = new Calculator(functions);
        calculator.AddFunction(new Function("+", 2,2, Associativity.Left, args => args[1] + args[0]));
        calculator.AddFunction(new Function("-", 2,2, Associativity.Left, args => args[1] - args[0]));
        calculator.AddFunction(new Function("*", 2,3, Associativity.Left, args => args[1] * args[0]));
        calculator.AddFunction(new Function("/", 2,3, Associativity.Left, args => args[1] / args[0]));
        calculator.AddFunction(new Function("^", 2,4, Associativity.Right, args => Math.Pow(args[1],args[0])));
        calculator.AddFunction(new Function("(", 0,0, Associativity.Left, args => 0));
        calculator.AddFunction(new Function(")", 0,0,Associativity.Left, args => 0));
        calculator.AddFunction(new Function("max",2,9, Associativity.Left, args => Math.Max(args[1],args[0])));
        calculator.AddFunction(new Function("=",0,0,Associativity.Left,args => 0));
        calculator.AddFunction(new Function("abs",1,0,Associativity.Left,args => Math.Abs(args[0])));
        calculator.AddFunction(new Function("A",0,4,Associativity.Left,args => 10));
        calculator.AddFunction(new Function("Maxim", 3 ,4,Associativity.Left,args => args[0] + args[1] * args[2] - 10));
        calculator.AddFunction(new Function("f", 3, 4, Associativity.Left, args => calculator.CalculateFunction("f(x,y) = x^2 + y ^ 2",args)));

        bool running = true;
        while (running)
        {
            Console.WriteLine("If you want to exit write 'quit'");
            Console.Write(">");
            string input = Console.ReadLine();
            if (input == "quit") break;
            if (calculator.CheckFunction(input))
            {
                continue;
            }
            
            if (calculator.CheckExpression(input)) Console.WriteLine(calculator.Calculate(input));

            
            
        }
    }
}

// queue - [smth smth func ( 1 2 4 ) smth smth smth] -> 
// queue - [smth smth 1 + 2 + 4 smth smth smth]