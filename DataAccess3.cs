using System;
using System.Collections.Generic;

/** 
 * This class only for testing purposes
 * Some parts are in comments because the interface has changed
 * If you want to use these functions you have to edit them to match the interface
 */
public class DataAccess3 : IDataAccess
{
	private List<Game> games;
	private List<Developer> developers;
	
	public DataAccess3()
	{
		developers = new List<Developer>();
        developers.Add(new Developer(1, "Rockstar Games"));
        developers.Add(new Developer(2, "Ubisoft"));
        developers.Add(new Developer(3, "Valve"));
        developers.Add(new Developer(4, "EA"));

		games = new List<Game>();
        games.Add(new Game(1, "Grand Theft Auto IV", developers[0]));
        games.Add(new Game(2, "Grand Theft Auto V", developers[0]));
        games.Add(new Game(3, "Assassin's Creed: Origins", developers[1]));
        games.Add(new Game(4, "Anno 1404", developers[1]));
        games.Add(new Game(5, "Counter-Strike: Global Offensive", developers[2]));	
		games.Add(new Game(6, "FIFA 15", developers[3]));	
		games.Add(new Game(7, "FIFA 16", developers[3]));	
		games.Add(new Game(8, "FIFA 17", developers[3]));	
		games.Add(new Game(9, "FIFA 18", developers[3]));	
	}

    public bool AddGame(string name, string developerName)
    { 	
		games.Add(new Game(name, new Developer(developerName)));
        return true;
	}

    public bool AddDeveloper(string name)
    { 
		developers.Add(new Developer(name));
        return true;
	}

    public bool EditGame(string newName, string oldName)
    { 
		//foreach(Game game_ in games)
		//{
		//	if(game_.ID == game.ID) 
		//	{
		//		games[games.IndexOf(game_)].Name = game.Name;
		//		break;
		//	}
		//}
        return true;
	}

    public bool EditDeveloper(string newName, string oldName)
    { 	
		//foreach(Developer developer_ in developers)
		//{
		//	if(developer_.ID == developer.ID) 
		//	{
		//		developers[developers.IndexOf(developer_)].Name = developer.Name;
		//		break;
		//	}
		//}
        return true;
	}

    public bool DeleteGame(string name)
    { 
		foreach(Game game in games)
		{
			if(game.Name == name)
			{
				games.RemoveAt(games.IndexOf(game));
				break;
			}
		}
        return true;
	}

    public bool DeleteDeveloper(string name)
    { 
		foreach(Developer developer in developers)
		{
			if(developer.Name == name)
			{
				developers.RemoveAt(developers.IndexOf(developer));
				break;
			}
		}
        return true;
	}

    public List<Game> GetGames()
    {
        return games;
    }

    public List<Developer> GetDevelopers()
    {
        return developers;
    }
}
