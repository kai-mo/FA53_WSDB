using System;
using System.Collections.Generic;

public interface IDataAccess
{
    void addGame(string name);

    void addDeveloper(string name);

    void editGame(Game game);

    void editDeveloper(Developer developer);

    void deleteGame(int id);

    void deleteDeveloper(int id);

    List<Game> getGames();

    List<Developer> getDevelopers();
}