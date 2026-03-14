namespace Calculator;

public class Checker
{
    public static bool CheckForFunctionDefinition(string expression, OwnList<Function> functions,out Function function)
    {
        function = default(Function);
        // we want 'f ( xzdf sfsdsy sfsfdsdz ) = x ^ 2 + y + z'
        OwnList<string> symbols = functions.GetSpecificArguments(function => function.Symbol);
        OwnQueue<string> tokens = Tokenizer.Tokenize(expression,skippableSymbols:new char[1]{' '});
        string name;
        int numberOfArhuments;

        string functionExpression = "";
        for (int index = expression.IndexOf("=") + 1; index < expression.Length; index++)
        {
            functionExpression += expression[index];
        }
        
        name = tokens.Pop();
        if (symbols.Contains(name) || tokens.Size == 0)
        {
            return false;
        }
        
        string value = tokens.Pop();
        
        if (value == "=")
        {
            Calculator calculator = new Calculator(functions);
            function = new Function(name,0,9,Associativity.Right,args => calculator.CalculateFunction(expression,args) );
            return Check(Tokenizer.Tokenize(functionExpression), functions);
        }

        if (value != "(")
        {
            return false;
        }
        return true;

    }
    public static bool Check(OwnQueue<string> tokens, OwnList<Function> functions)
    {
        OwnList<string> functionSymbols = functions.GetSpecificArguments(operation => operation.Symbol);
        int bracketsCounter = 0;
        for (int index = 0; index < tokens.Size; index++)
        {
            string value = tokens.Pop();
            if (Double.TryParse(value,out var result)) continue;
            
            if (!functionSymbols.Contains(value)) return false;
            if (value == ")") bracketsCounter--;
            if (value == "(") bracketsCounter++;
            if (bracketsCounter < 0) return false;
            
        }

        if (bracketsCounter != 0) return false;
        return true;
    }
}