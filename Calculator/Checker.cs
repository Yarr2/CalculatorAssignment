namespace Calculator;

public class Checker
{
    public static bool Check(OwnQueue<string> tokens, OwnList<BinaryOperation> operations)
    {
        OwnList<string> operationSymbols = operations.GetSpecificArguments(operation => operation.Symbol);
        int bracketsCounter = 0;
        for (int index = 0; index < tokens.Size; index++)
        {
            string value = tokens.Pop();
            if (!Double.TryParse(value,out var result)) return false;
            if (!operationSymbols.Contains(value)) return false;
            if (value == ")") bracketsCounter--;
            if (value == "(") bracketsCounter++;
            if (bracketsCounter < 0) return false;
            
        }

        if (bracketsCounter != 0) return false;
        return true;
    }
}