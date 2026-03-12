namespace Calculator;

public interface Operator
{
    public string Symbol();
    public double Calculate(double[] arguments);
}