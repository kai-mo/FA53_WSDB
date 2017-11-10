using System;
using System.Collections.Generic;

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

    public void addGame(string name)
    { 	
		games.Add(new Game(name));
	}

    public void addDeveloper(string name)
    { 
		developers.Add(new Developer(name));
	}

    public void editGame(Game game)
    { 
		foreach(Game game_ in games)
		{
			if(game_.ID == game.ID) 
			{
				games[games.IndexOf(game_)].Name = game.Name;
				break;
			}
		}
	}

    public void editDeveloper(Developer developer)
    { 	
		foreach(Developer developer_ in developers)
		{
			if(developer_.ID == developer.ID) 
			{
				developers[developers.IndexOf(developer_)].Name = developer.Name;
				break;
			}
		}
	}

    public void deleteGame(int id)
    { 
		foreach(Game game in games)
		{
			if(game.ID == id)
			{
				games.RemoveAt(games.IndexOf(game));
				break;
			}
		}
	}

    public void deleteDeveloper(int id)
    { 
		foreach(Developer developer in developers)
		{
			if(developer.ID == id)
			{
				developers.RemoveAt(developers.IndexOf(developer));
				break;
			}
		}
	}

    public List<Game> getGames()
    {
        return games;
    }

    public List<Developer> getDevelopers()
    {
        return developers;
    }
}
