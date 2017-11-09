using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AbstractBusinessLayer : IBusinessLayer
{
    protected IDataAccess dataAccess;

    public AbstractBusinessLayer(IDataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    public void addGame(string name)
    {
        this.dataAccess.addGame(name);
    }

    public void addDeveloper(string name)
    {
        this.dataAccess.addDeveloper(name);
    }

    public void editGame(int id, string name)
    {
        Game game = new Game(id, name);
        this.dataAccess.editGame(game);
    }

    public void editDeveloper(int id, string name)
    {
        Developer developer = new Developer(id, name);
        this.dataAccess.editDeveloper(developer);
    }

    public void deleteGame(int id)
    {
        this.dataAccess.deleteGame(id);
    }

    public void deleteDeveloper(int id)
    {
        this.dataAccess.deleteDeveloper(id);
    }

    public abstract List<Game> getGames();

    public abstract List<Developer> getDevelopers();

    public List<Game> sortGamesList(List<Game> list, string order)
    {
        List<Game> sortedList;
        if (order == "DESC")
        {
            sortedList = list.OrderByDescending(item => item.Name).ToList();
        }
        else
        {
            sortedList = list.OrderBy(item => item.Name).ToList();
        }
        return sortedList;
    }

    public List<Developer> sortDevelopersList(List<Developer> list, string order)
    {
        List<Developer> sortedList;
        if (order == "DESC")
        {
            sortedList = list.OrderByDescending(item => item.Name).ToList();
        }
        else
        {
            sortedList = list.OrderBy(item => item.Name).ToList();
        }
        return sortedList;
    }
}