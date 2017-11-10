using System;
using System.Windows.Forms;

public class GamesManager
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            ShowViewError();
            return;
        }

        switch (args[0].ToLower())
        {
            case "tui": new TUI(new BusinessLayer1(new DataAccess3())); break;
            case "gui": Application.Run(new GUI()); break;
            default: ShowViewError(); return;
        }
    }

    private static void ShowViewError()
    {
        Console.WriteLine("Please enter a valid view option [tui|gui]!");
    }
}
