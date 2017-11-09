using System;
using System.Collections.Generic;

public interface IBusinessLayer
{
    void addGame(string name);

    void addDeveloper(string name);

    void editGame(int id, string name);

    void editDeveloper(int id, string name);

    void deleteGame(int id);

    void deleteDeveloper(int id);

    List<Game> getGames();

    List<Developer> getDevelopers();
}