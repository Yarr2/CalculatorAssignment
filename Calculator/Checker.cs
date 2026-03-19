using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Calculator;

public class Checker
{
    public static bool CheckForFunctionDefinition(string expression, OwnList<Function> functions,out Function function)
    {
        function = default(Function);
        Calculator calculator = new Calculator(functions);
        // we want 'f ( ( xzdf ) , ( sfsdsy ) , ( sfsfdsdz ) ) = x ^ 2 + y + z'
        OwnList<string> symbols = functions.GetSpecificArguments(tempFunction => tempFunction.Symbol);
        OwnQueue<string> tokens = Tokenizer.Tokenize(expression,skippableSymbols:new char[1]{' '});
        string name;
        int numberOfArguments = 0;
        
        string functionExpression = "";
        for (int index = expression.IndexOf("=") + 1; index < expression.Length; index++)
        {
            functionExpression += expression[index];
        }
        
        
        name = tokens.Pop();
        if (!name.All(char.IsLetter)) {Console.WriteLine("Invalid name for function");return false;}
        if (symbols.Contains(name))
        {
            Console.WriteLine("function already implemented");
            return false;
        }

        if (tokens.Size == 0)
        {
            Console.WriteLine("Not enough info for definition");
        }
        
        string value = tokens.Pop();
        
        if (value == "=")
        {
            function = new Function(name,0,9,Associativity.Right,args => calculator.CalculateFunction(expression,args) ,TypeOfFunction.Operation);
            Console.WriteLine("try to define variable");
            return Check(functionExpression, functions);
        }

        if (value != "(")
        {
            Console.WriteLine($"After name does not come '(' but comes '{value}'");
            return false;
        }
        // at this point tokens should look like - '( xzdf ) , ( sfsdsy ) , ( sfsfdsdz ) ) = x ^ 2 + y + z'
        Console.WriteLine("Start Tokens");tokens.ShowQueue();Console.WriteLine("End tokens");
        while (value != "=")
        {
            // in loop, we aim to check expression of type ( text )
            if (tokens.Pop() != "(") { return false; }

            if (!tokens.Pop().All(char.IsLetter)) return false;

            if (tokens.Pop() != ")") return false;

            value = tokens.Pop();
            numberOfArguments++;
            if (value == ")" && (tokens.Pop() == "=")) break;
            if (value != ",") return false;
            
        }
        function = new Function(name,numberOfArguments,9,Associativity.Right,args => calculator.CalculateFunction(expression,args),TypeOfFunction.Function );
        return true; 

    }
    
    public static bool CheckFunctionValidity(string expression, int numberOfArguments, OwnList<Function> functions)
    {
        OwnQueue<string> tokens = Tokenizer.Tokenize(expression,skippableSymbols:new char[1]{' '});
        // expected input f ( ( x ) , ( 1 + 2 + max(1,2)) * 54,23 ) => correct , x => correct (x has 0 arguments)
        // f ( x ( 1 + 2 ) * 54 23 ) => ( x ( 1 + 2 ) * 54 23 )
        int realNumberOfArguments = 0;
        tokens.Pop();
        if (tokens.Pop() != "(") {Console.WriteLine("Invalid start");return false;}
        
        while (tokens.Size != 1)
        {
            string value = tokens.Pop();
            string argument = "";
            int brackets;
            if (value == "(")
            {
                brackets = 1;
            }
            else
            {
                Console.WriteLine("incorrect start of an argument");
                return false;
            }

            argument += value;
            value = tokens.Pop();
            while (brackets != 0 && tokens.Size != 1)
            {
                if (value == "(") brackets++;
                if (value == ")") brackets--;
                argument += value;
                value = tokens.Pop();
            }

            if (value != ",")argument += value;
            if (!Check(argument, functions)) {Console.WriteLine($"Wrong argument - {argument}");return false;}
            realNumberOfArguments++;
        }

        if (tokens.Pop() == ")" && numberOfArguments == realNumberOfArguments) return true;
        return false;


    }
    public static bool Check(string expression, OwnList<Function> functions)
    {
        // expression in form of f(x,y) ^ (2 + g(t))
        OwnQueue<string> tokens = Tokenizer.Tokenize(expression, skippableSymbols:new char[1]{' '});
        // Console.WriteLine("Start tokens Check");tokens.ShowQueue();Console.WriteLine("End tokens Check");
        OwnList<string> functionSymbols = functions.GetSpecificArguments(operation => operation.Symbol);
        int bracketsCounter = 0;
        int size = tokens.Size;
        int index = 0;
        while (index < size)
        {
            string value = tokens.Pop();
            index++;
            if (Double.TryParse(value.Replace(".",","),out var result)) continue;
            if (value == ",")
            {
                continue;
            }// will be fixed
            if (value == ")")
            {
                bracketsCounter--;
                continue;
            }
            if (value == "(")
            {
                bracketsCounter++;
                continue;
            }
            if (bracketsCounter < 0)
            {
                Console.WriteLine("brackets mismatched");
                return false;
            }
            if (!functionSymbols.Contains(value))
            {
                Console.WriteLine($"Function '{value}' is not implemented");
                return false;
            }
            // now we have something like this f(1,2,3,4) 
            Function function = functions.GetElement(function => function.Symbol, value);
            if (function.NumberOfArguments == 0 || function.TypeOfFunction == TypeOfFunction.Operation) continue;
            if (tokens.Pop() != "(")
            {
                Console.WriteLine("Incorrect syntax of function");
                return false;
            }

            index++;

            string functionExpression = "";
            functionExpression += function.Symbol;
            functionExpression += "(";
            int currentbracketsCounter = bracketsCounter;
            bracketsCounter++;
            while (bracketsCounter != currentbracketsCounter)
            {
                int smth = tokens.Size;
                string element = tokens.Pop();
                index++;
                functionExpression += element;
                if (element == "(") bracketsCounter++;
                if (element == ")") bracketsCounter--;
            }

            if (!CheckFunctionValidity(functionExpression, function.NumberOfArguments, functions))
            {
                Console.WriteLine(functionExpression);
                Console.WriteLine("Incorrect function usage");
                return false;
            }


        }

        if (bracketsCounter != 0)
        {
            Console.WriteLine("Brackets mismatched");
            return false;
        }
        return true;
    }
}



// problem with this expression f(g(1),f(2,3)) 
