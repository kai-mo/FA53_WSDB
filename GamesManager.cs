using System;

public class GamesManager
{
    public static void Main(string[] args)
    {
        if (args.Length == 0 || !args[0].ToLower().Equals("tui"))
        {
            Console.WriteLine("Please enter a valid option [tui]!");
        }
        else
        {
            new TUI();
        }
    }
}
