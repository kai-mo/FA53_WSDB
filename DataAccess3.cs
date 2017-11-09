using System;
using System.Collections.Generic;

public class DataAccess3 : IDataAccess
{
    public void addGame(string name)
    { }

    public void addDeveloper(string name)
    { }

    public void editGame(Game game)
    { }

    public void editDeveloper(Developer developer)
    { }

    public void deleteGame(int id)
    { }

    public void deleteDeveloper(int id)
    { }

    public List<Game> getGames()
    {
        List<Game> games = new List<Game>();
        games.Add(new Game("EGameFive"));
        games.Add(new Game("AGameOne"));
        games.Add(new Game("BGameTwo"));
        games.Add(new Game("DGameFour"));
        games.Add(new Game("CGameThree"));
        return games;
    }

    public List<Developer> getDevelopers()
    {
        List<Developer> developers = new List<Developer>();
        developers.Add(new Developer("EDeveloperOne"));
        developers.Add(new Developer("ADeveloperTwo"));
        developers.Add(new Developer("BDeveloperThree"));
        developers.Add(new Developer("DDeveloperFour"));
        developers.Add(new Developer("CDeveloperFive"));
        return developers;
    }
}
