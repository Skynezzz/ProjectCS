using Engine;

class Program
{
    static void Main(string[] args)
    {
        string draw = "";

        for (int i = 0; i < 12; i++)
        {
            draw += "OOOOOOOOO0";
        }
        Console.WriteLine(draw);
        for (int i = 0; i < 30; i++) 
        {
            Console.WriteLine(i+1);
        }
    }
}