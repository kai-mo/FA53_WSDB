using System;
using System.Collections.Generic;

public class DataAccess3 : IDataAccess
{
	private List<Game> games;
	private List<Developer> developers;
	
	public DataAccess3()
	{
		games = new List<Game>();
        games.Add(new Game(1, "EGameFive"));
        games.Add(new Game(2, "AGameOne"));
        games.Add(new Game(3, "BGameTwo"));
        games.Add(new Game(4, "DGameFour"));
        games.Add(new Game(5, "CGameThree"));
		
		developers = new List<Developer>();
        developers.Add(new Developer(1, "EDeveloperOne"));
        developers.Add(new Developer(2, "ADeveloperTwo"));
        developers.Add(new Developer(3, "BDeveloperThree"));
        developers.Add(new Developer(4, "DDeveloperFour"));
        developers.Add(new Developer(5, "CDeveloperFive"));
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
    { }

    public void deleteGame(int id)
    { }

    public void deleteDeveloper(int id)
    { }

    public List<Game> getGames()
    {
        return games;
    }

    public List<Developer> getDevelopers()
    {
        return developers;
    }
}
