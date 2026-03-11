namespace Calculator;

public class ToPostFixConvertor
{
    // Queue tokens => Queue sorted tokens
    public static OwnQueue<string> ToPostFixConvert(OwnQueue<string> tokens, OwnList<BinaryOperation> avaliableOperations)
    {
        OwnQueue<string> postfix = new OwnQueue<string>(tokens.Size);
        OwnStack<BinaryOperation> operations = new OwnStack<BinaryOperation>(tokens.Size);
        int size = tokens.Size;
        for (int index = 0; index < size; index ++)
        {
            string value = tokens.Pop();
            if (Double.TryParse(value, out double result))
            {
                postfix.Add(value);
                continue;
            }

            if (value == ")")
            {
                value = operations.Pull().Symbol;
                while (value != "(" && operations.StackSize >= 1)
                {
                    postfix.Add(value);
                    value = operations.Pull().Symbol;
                }
                if (value != "(") postfix.Add(value);
                continue;
            }
            while (value != "(" && BinaryOperation.Comparator(avaliableOperations.GetElement(operation => operation.Symbol, value), operations.Peek()))
            {
                BinaryOperation operation = operations.Pull();
                postfix.Add(operation.Symbol);
            }
            operations.Push(avaliableOperations.GetElement(operation => operation.Symbol,value));
            Console.WriteLine("-----------------------");
            operations.ShowStack();
            
        }
        while (!operations.IsEmpty())
        {
            BinaryOperation value = operations.Pull();
            postfix.Add(value.Symbol);
        }

        return postfix;
    }
}