namespace Calculator;

public class Tokenizer
{
    public static OwnQueue<string> Tokenize(string text, List<BinaryOperation> operations)
    {
        
        OwnQueue<string> queue = new OwnQueue<string>(text.Length);
        List<string> operationSymbols = operations.Select(operation => operation.Symbol).ToList();
        string number = "";
        foreach (char symbol in text)
        {
            if (symbol == ' ') continue;
            if (symbol <= '9' && symbol >= '0')
            {
                number += symbol;
                continue;
            }
            if ( operationSymbols.Contains(symbol.ToString()))
            {
                if (number != "") queue.Add(number);
                queue.Add(symbol.ToString());
                number = "";
                continue;
            }

            throw new Exception("Our calculator does not support your symbols");
        }

        if (number != "") queue.Add(number);
        return queue;
    }
}