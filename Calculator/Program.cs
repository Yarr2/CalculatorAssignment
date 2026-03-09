namespace Calculator;

class Program
{
    static void Main(string[] args)
    {
        var stack = new OwnQueue<string>(5);
        stack.Add("loh1");
        stack.Add("loh2");
        stack.Add("loh3");
        stack.Add("loh4");
        stack.Add("loh5");
        stack.ShowQueue();
        stack.Pop();
        Console.WriteLine(stack.Pop());
        stack.ShowQueue();

    }
}