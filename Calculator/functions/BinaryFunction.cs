using System.Reflection.Metadata;

namespace Calculator;

public class BinaryFunction : Function
{
    public BinaryFunction(string name, string implementation) : base(name,implementation,2)
    { }
    //f(x,y) = x^ 2 + y * x  - 5
}