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
		
		switch (args[2])
		{
			// TODO: implement DataAccess1 and DataAccess2 classes
			//case "1": dataAccess = new DataAccess1(); break;
			//case "2": dataAccess = new DataAccess2(); break;
			case "3": dataAccess = new DataAccess3(); break;
			default: ShowDataAccessError(); return;
		}

		IBusinessLayer businessLayer;
		
		switch (args[1])
		{
			case "1": businessLayer = new BusinessLayer1(dataAccess); break;
			case "2": businessLayer = new BusinessLayer2(dataAccess); break;
			default: ShowBusinessLayerError(); return;
		}
		
        switch (args[0].ToLower())
        {
            case "tui": new TUI(businessLayer); break;
            case "gui": Application.Run(new GUI()); break;
            default: ShowViewError(); return;
        }
    }

	private static void ShowArgumentError()
	{
		Console.WriteLine("Please enter three arguments!");
	}
	
	private static void ShowDataAccessError()
	{
		Console.WriteLine("Please enter a valid data access option [1|2|3]!");
	}
	
	private static void ShowBusinessLayerError()
	{
		Console.WriteLine("Please enter a valid businesslayer option [1|2]!");
	}
	
    private static void ShowViewError()
    {
        Console.WriteLine("Please enter a valid view option [tui|gui]!");
    }
	

}
