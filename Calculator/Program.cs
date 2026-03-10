namespace Calculator;

class Program
{
    static void Main(string[] args)
    {
        OwnList<string> smth = new OwnList<string>();
        smth.Append("Yarema");
        smth.Append("Anna");
        smth.Append("Ignat");
        smth.Append("Rostik");
        smth.Append("Lina");
        smth.Insert("Marat",3);
        smth.Remove("Marat");
        smth.ShowList();
    }
}