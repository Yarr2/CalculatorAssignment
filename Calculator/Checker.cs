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
        int numberOfArhuments = 0;
        
        
        
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
            Console.WriteLine($"After name does not come '(' but comes {value}");
            return false;
        }
        // at this point tokens should look like - '( xzdf ) , ( sfsdsy ) , ( sfsfdsdz ) ) = x ^ 2 + y + z'
        Console.WriteLine("Start Tokens");tokens.ShowQueue();Console.WriteLine("End tokens");
        while (value != ")")
        {
            if (tokens.Pop() != "(") return false;
            if (!value.All(char.IsLetter))
            {
                Console.WriteLine($"Argument = {value}");
                Console.WriteLine("Function argument is incorrect(not letters)");
                return false;}

            numberOfArhuments++;
            
            if (tokens.Pop() != ")") return false;
            value = tokens.Pop();
            if (value == ",")
            {
                value = tokens.Pop();
                continue;
            };
            if (value == ")") break;
            Console.WriteLine("Wrong formatting");
            return false;
        }
        function = new Function(name,numberOfArhuments,9,Associativity.Right,args => calculator.CalculateFunction(expression,args),TypeOfFunction.Function );
        return true; 

    }

    public static bool CheckFunctionValidity(string expression, int numberOfArguments, OwnList<Function> functions)
    {
        OwnQueue<string> tokens = Tokenizer.Tokenize(expression,skippableSymbols:new char[1]{' '});
        // expected input f ( x , ( 1 + 2 ) * 54,23 ) => correct , x => correct (x has 0 arguments)
        // f ( x ( 1 + 2 ) * 54 23 ) => ( x ( 1 + 2 ) * 54 23 )
        tokens.ShowQueue();
        int realNumberOfArguments = 0;
        tokens.Pop();
        if (tokens.Pop() != "(") return false;
        while (tokens.Size != 1)
        {
            string value = tokens.Pop();
            string argument = "";
            while (value != "," && tokens.Size != 1)
            {
                argument += value;
                value = tokens.Pop();
            }

            if (value != ",")argument += value;
            if (!Check(argument, functions)) return false;
            realNumberOfArguments++;
        }

        if (tokens.Pop() == ")" && numberOfArguments == realNumberOfArguments) return true;
        return false;


    }
    public static bool Check(string expression, OwnList<Function> functions)
    {
        // expression in form of f(x,y) ^ (2 + g(t))
        OwnQueue<string> tokens = Tokenizer.Tokenize(expression, skippableSymbols:new char[1]{' '});
        OwnList<string> functionSymbols = functions.GetSpecificArguments(operation => operation.Symbol);
        int bracketsCounter = 0;
        int size = tokens.Size;
        int index = 0;
        while (index < size)
        {
            string value = tokens.Pop();
            index++;
            if (Double.TryParse(value.Replace(".",","),out var result)) continue;
            if (value == ",") continue;// will be fixed
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
                string element = tokens.Pop();
                index++;
                functionExpression += element;
                if (element == "(") bracketsCounter++;
                if (element == ")") bracketsCounter--;
            }

            if (!CheckFunctionValidity(functionExpression, function.NumberOfArguments, functions))
            {
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