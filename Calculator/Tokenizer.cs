namespace Calculator;

public class Tokenizer
{
    public static OwnQueue<string> Tokenize(string text, OwnList<BinaryOperation> operations)
    {
        text += " ";
        OwnQueue<string> queue = new OwnQueue<string>(text.Length);
        OwnList<string> operationSymbols = operations.GetSpecificArguments(operation => operation.Symbol);
        int index = 0;
        string number = "";
        string textToken = "";
        while (index < text.Length)
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
                continue;
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
        }
        return queue;
    }
}