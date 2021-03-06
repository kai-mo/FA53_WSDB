﻿using System.Collections.Generic;

namespace GamesManager
{
    public interface IDataAccess
    {
        bool AddGame(string name, string developerName);

        bool AddDeveloper(string name);

        bool EditGame(string newName, string oldName);

        bool EditDeveloper(string newName, string oldName);

        bool DeleteGame(string name);

        bool DeleteDeveloper(string name);

        Developer GetDeveloper(string name);

        List<Game> GetGames();

        List<Developer> GetDevelopers();
    }
}