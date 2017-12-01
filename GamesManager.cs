using System;
using System.Windows.Forms;

public class GamesManager
{
    public static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            ShowArgumentError();
            return;
        }

		IDataAccess dataAccess;
		
		switch (args[2].ToLower())
		{
            case "sqlite": dataAccess = new DataAccess1(); break;
            case "json": dataAccess = new DataAccess2(); break;
			default: ShowArgumentError(); return;
		}

		IBusinessLayer businessLayer;
		
		switch (args[1].ToLower())
		{
			case "asc": businessLayer = new BusinessLayer1(dataAccess); break;
			case "desc": businessLayer = new BusinessLayer2(dataAccess); break;
			default: ShowArgumentError(); return;
		}
		
        switch (args[0].ToLower())
        {
            case "tui": new TUI(businessLayer); break;
            case "gui": Application.Run(new GUI(businessLayer)); break;
            default: ShowArgumentError(); return;
        }
    }

	private static void ShowArgumentError()
	{
        Console.WriteLine("Invalid arguments!\n");
        Console.WriteLine("Usage:\n");
        Console.WriteLine("GamesManager.exe (tui|gui) (asc|desc) (sqlite|json)");
    }
}
