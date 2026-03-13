namespace Calculator;

public class ToPostFixConvertor
{
    // Queue tokens => Queue sorted tokens
    public static OwnQueue<string> ToPostFixConvert(OwnQueue<string> tokens,
        OwnList<Function> avaliableFunctions)
    {
        OwnQueue<string> postfix = new OwnQueue<string>(tokens.Size);
        OwnStack<Function> functions = new OwnStack<Function>(tokens.Size);
        int size = tokens.Size;
        for (int index = 0; index < size; index++)
        {
            string value = tokens.Pop();
            if (Double.TryParse(value, out double result))
            {
                postfix.Add(value);
                continue;
            }

            if (value == ")")
            {
                value = functions.Pull().Symbol;
                while (value != "(" && functions.StackSize >= 1)
                {
                    postfix.Add(value);
                    value = functions.Pull().Symbol;
                }

                if (value != "(") postfix.Add(value);
                continue;
            }

            while (value != "(" &&
                   Function.Comparator(avaliableFunctions.GetElement(function => function.Symbol, value),
                       functions.Peek()))
            {
                Function function = functions.Pull();
                postfix.Add(function.Symbol);
            }

            functions.Push(avaliableFunctions.GetElement(operation => operation.Symbol, value));

        }

        while (!functions.IsEmpty())
        {
            Function value = functions.Pull();
            postfix.Add(value.Symbol);
        }
   
        return postfix;
    }
}
    