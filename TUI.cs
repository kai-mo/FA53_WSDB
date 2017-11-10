using System;
using System.Collections.Generic;

﻿public class TUI
{
	private IBusinessLayer businesslayer;
	
    public TUI(IBusinessLayer businesslayer)
	{
		this.businesslayer = businesslayer;
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
            case "b": AddGame(); break;
            case "e": ShowAllDevelopers(); break;
			case "f": AddDeveloper(); break;
			case "q": return;
			default : ShowMenu(); break;
		}
	}

    private void ShowAllGames()
    {
        Console.Clear();

        foreach(Game game in businesslayer.getGames())
        {
            Console.WriteLine("- " + game.Name);
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();

        ShowMenu();
    }

    private void ShowAllDevelopers()
    {
        Console.Clear();

        foreach (Developer developer in businesslayer.getDevelopers())
        {
            Console.WriteLine("- " + developer.Name);
        }

        Console.WriteLine("\nPress any key to return to menu...");
        Console.ReadKey();

        ShowMenu();
    }

    private void AddGame()
    {
        string new_game;

        do
        {
            Console.Clear();
            Console.Write("Enter name of new game or q to abort: ");
            new_game = Console.ReadLine();
        } while (new_game.Equals(""));

        if (!new_game.Equals("q"))
        {
            // TODO: implement addGame() in dataAccess
        }

        ShowMenu();
    }
	
	private void AddDeveloper()
	{
		string new_developer;
		
        do
        {
            Console.Clear();
            Console.Write("Enter name of new developer or q to abort: ");
            new_developer = Console.ReadLine();
        } while (new_developer.Equals(""));

        if (!new_developer.Equals("q"))
        {
            // TODO: implement addDeveloper() in dataAccess
        }

        ShowMenu();
	}
}
