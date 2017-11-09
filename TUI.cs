using System;
using System.Collections.Generic;

﻿public class TUI
{
	public TUI()
	{
		ShowMenu();
	}

	private void ShowMenu()
	{
		Console.Clear();
		
		Console.Write(
@"GAMES MANAGER

Show Games          (a)
New Game            (b)
Edit Game           (c)
Delete Game         (d)
-----------------------
Show Developers     (e)
New Developer       (f)
Edit Developer      (g)
Delete Developer    (h)
-----------------------
Show Assignments    (i)
Manage Assignments  (j)
-----------------------
Exit                (q)

Choose an option: ");

		string choice = Console.ReadLine();

		switch( choice ){
            case "a": ShowAllGames(); break;
            case "e": ShowAllDevelopers(); break;
			case "q": return;
			default : ShowMenu(); break;
		}
	}

    private void ShowAllGames()
    {
        Console.Clear();

        List<String> games = new List<string>();
        games.Add("CS:GO");
        games.Add("The Witcher 3");
        games.Add("Grand Theft Auto V");

        foreach(String game in games)
        {
            Console.WriteLine("- " + game);
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();

        ShowMenu();
    }

    private void ShowAllDevelopers()
    {
        Console.Clear();

        List<String> developers = new List<string>();
        developers.Add("Valve");
        developers.Add("Ubisoft");
        developers.Add("Rockstar Games");

        foreach (String developer in developers)
        {
            Console.WriteLine("- " + developer);
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();

        ShowMenu();
    }
}
