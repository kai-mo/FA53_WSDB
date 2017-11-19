using System;
using System.Collections.Generic;

public interface IDataAccess
{
    bool AddGame(string name, string developerName);

    bool AddDeveloper(string name);

    bool EditGame(Game game, string oldName);

    bool EditDeveloper(Developer developer, string oldName);

    bool DeleteGame(string name);

    bool DeleteDeveloper(string name);

    List<Game> GetGames();

    List<Developer> GetDevelopers();
}