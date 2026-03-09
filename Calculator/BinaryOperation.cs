namespace Calculator;

public class BinaryOperation
{
    public readonly string Symbol;
    public readonly int Preference;
    public readonly string Assosiativity;
    public readonly Func<double[], double> _calculate;

    public BinaryOperation(string symbol, int preference, string assosiativity, Func<double[],double> calculate)
    {
        Symbol = symbol;
        Preference = preference;
        Assosiativity = assosiativity;
        _calculate = calculate;
    }
    public override string ToString()
    {
        return $"Operation '{Symbol}' with preference value {Preference},associativity - {Assosiativity}";
    }
}
