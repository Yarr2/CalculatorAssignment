namespace Calculator;

public class Tokenizer
{
    public static OwnQueue<string> Tokenize(string text,char[] skippableSymbols = null)
    {
        if (skippableSymbols == null)
        {
            skippableSymbols = new char[2] { ' ', ',' };
        }
        text += " ";
        OwnQueue<string> queue = new OwnQueue<string>(text.Length);
        int index = 0;
        string number = "";
        string textToken = "";
        while (index < text.Length)
        {
            char symbol = text[index];

            if (skippableSymbols.Contains(symbol))
            {
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
                continue;
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
                continue;
            }
            queue.Add(char.ToString(symbol));
            index++;

        }
        return queue;
    }
}