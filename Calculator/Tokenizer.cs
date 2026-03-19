namespace Calculator;

public class Tokenizer
{
    public static string AddBracketsForFunctions(string text)
    {
        text = text.Replace("(", "((");
        text = text.Replace(")", "))");
        text = text.Replace(",", "),(");
        return text;
    }
    public static OwnQueue<string> Tokenize(string text,char[]? skippableSymbols = null)
    {
        
        bool IsSkippableSymbolsDefined = true;
        if (skippableSymbols == null)
        {
            
            skippableSymbols = new char[1] { ' ' };
            IsSkippableSymbolsDefined = false;
        }
        text += " ";
        OwnQueue<string> queue = new OwnQueue<string>(3 * text.Length);
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

            while (('0' <= text[index] && text[index] <= '9') || text[index] == '.')
            {
                if (number.Contains('.') && text[index] == '.') break;
                
                number += text[index];
                index++;
            }

            if (number != "")
            {
                if (number[number.Length - 1] == '.') number.Replace(".", "");
                number.Replace(".", ",");
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


            if ((IsSkippableSymbolsDefined && text[index] == ',') || text[index] != ',')
            {
                queue.Add(char.ToString(symbol));
            }

            index++;

        }
        return queue;
    }
}