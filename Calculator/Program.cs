using System.ComponentModel.DataAnnotations;


namespace Calculator;

class Program
{
    static void Main(string[] args) // "12 3 +" 2^3^2 -> 2 ^ (3 ^ 2) -> 2 ^ 9 -> 512
    {
        OwnList<Function> functions = new OwnList<Function>();
        Calculator calculator = new Calculator(functions);
        calculator.AddFunction(new Function("+", 2,2, Associativity.Left, args => args[0] + args[1],TypeOfFunction.Operation));
        calculator.AddFunction(new Function("-", 2,2, Associativity.Left, args => args[0] - args[1],TypeOfFunction.Operation));
        calculator.AddFunction(new Function("*", 2,3, Associativity.Left, args => args[0] * args[1],TypeOfFunction.Operation));
        calculator.AddFunction(new Function("/", 2,3, Associativity.Left, args => args[0] / args[1],TypeOfFunction.Operation));
        calculator.AddFunction(new Function("^", 2,4, Associativity.Right, args => Math.Pow(args[0],args[1]),TypeOfFunction.Operation));
        calculator.AddFunction(new Function("(", 0,0, Associativity.Left, args => 0,TypeOfFunction.Operation));
        calculator.AddFunction(new Function(")", 0,0,Associativity.Left, args => 0,TypeOfFunction.Operation));
        calculator.AddFunction(new Function("max",2,9, Associativity.Left, args => Math.Max(args[0],args[1]),TypeOfFunction.Function));
        calculator.AddFunction(new Function("=",0,0,Associativity.Left,args => 0,TypeOfFunction.Operation));
        calculator.AddFunction(new Function("abs",1,0,Associativity.Left,args => Math.Abs(args[0]),TypeOfFunction.Function));
        calculator.AddFunction(new Function("A",0,4,Associativity.Left,args => 10,TypeOfFunction.Operation));
        calculator.AddFunction(new Function("Maxim", 3 ,4,Associativity.Left,args => args[0] + args[1] * args[2] - 10,TypeOfFunction.Function));
        calculator.AddFunction(new Function("f", 2, 4, Associativity.Left, args => calculator.CalculateFunction("f((x),(y)) = x^2 + max((x),(y))",args),TypeOfFunction.Function));
        calculator.AddFunction(new Function("g", 1 ,4, Associativity.Left, args => calculator.CalculateFunction("g((x)) = f((x),(x))",args),TypeOfFunction.Function));

        bool running = true;
        bool ASTCheck = false;
        while (running)
        {
            if (!ASTCheck)Console.WriteLine("If you want to exit write 'quit'");
            Console.Write(">");
            string input = Console.ReadLine();
            
            input = Tokenizer.AddBracketsForFunctions(input);
            if (input == "quit") break;
            if (input == "functions")
            { 
                calculator.ShowFunctions();
                continue;
            }

            if (input == "AST")
            {
                ASTCheck = true;
                continue;
            }

            if (calculator.CheckFunction(input)) {
                continue;
            }

            if (calculator.CheckExpression(input))
            {
                if (ASTCheck) 
                {
                    calculator.CalculateWithAST(input);
                    ASTCheck = false;
                }
                else Console.WriteLine(calculator.Calculate(input));
            }
            // catch (Exception exception)
            // {
            //     if ((exception.Message) == "Something") continue;
            //     
            // }

        }
    }
}

// queue - [smth smth func ( 1 2 4 ) smth smth smth] -> 
// queue - [smth smth 1 + 2 + 4 smth smth smth]