using System;

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
			case "q": return;
			default : ShowMenu(); break;
		}
	}
}
