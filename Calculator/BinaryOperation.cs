namespace Calculator;

public enum Associativity
{
    Left,Right
}
public class BinaryOperation
{
    public readonly string Symbol;
    public readonly int Preference;
    public readonly Associativity Assosiativity;
    public readonly Func<double[], double> _calculate;

    public BinaryOperation(string symbol, int preference, Associativity assosiativity, Func<double[],double> calculate)
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
