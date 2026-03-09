namespace Calculator;

public class BinaryOperation
{
    public string Symbol;
    public readonly int Preference;
    public readonly string Assosiativity;
    public readonly Func<double[], double> calculate;

    public override string ToString()
    {
        return $"Operation '{Symbol}' with preference value {Preference},associativity - {Assosiativity}";
    }
}
