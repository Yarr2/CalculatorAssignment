namespace Calculator;

public class Tokenizer
{
    public static OwnQueue<string> Tokenize(string text, List<BinaryOperation> operations)
    {
        
        OwnQueue<string> queue = new OwnQueue<string>(text.Length);
        List<string> operationSymbols = operations.Select(operation => operation.Symbol).ToList();
        int index = 0;
        string number = "";
        string textToken = "";
        while (index <= text.Length)
        {
            char symbol = text[index];

            if (symbol == ' ' ||
                symbol == ',')
            {
                index++;
                continue;
            }
            if (operationSymbols.Contains(char.ToString(symbol)))
            {
                queue.Add(char.ToString(symbol));
                index++;
            }

            while ('0' <= text[index] && text[index] <= '9')
            {
                number += text[index];
                index++;
            }

            if (number != "")
            {
                queue.Add(number);
                number = "";
            }

            while ('a' <= char.ToLower(text[index]) && char.ToLower(text[index]) <= 'z')
            {
                textToken += text[index];
                index++;
            }

            if (textToken != "")
            {
                queue.Add(textToken);
                textToken = "";
            }

            Console.WriteLine("Start of an attempt ----------");
            queue.ShowQueue();
            Console.WriteLine(index);
            Console.WriteLine($"text.Length - {text.Length}");
            Console.WriteLine("End of an attempt ----------");
        }
        return queue;
    }
}