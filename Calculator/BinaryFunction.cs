using System.Reflection.Metadata;

namespace Calculator;

public class BinaryFunction : Function
{
    public BinaryFunction(string name, string implementation) : base(name,implementation,2)
    {
        // implementation => f(x,y) = x^3 + xyx + xx^2 + 5y
    }
    public override 
}