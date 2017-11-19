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

    public void AddGame(string name, string developerName)
    {
        this.dataAccess.AddGame(name, developerName);
    }

    public void AddDeveloper(string name)
    {
        this.dataAccess.AddDeveloper(name);
    }

    public void EditGame(string name, string oldName)
    {
        Game game = new Game(name);
        this.dataAccess.EditGame(game, oldName);
    }

    public void EditDeveloper(string name, string oldName)
    {
        Developer developer = new Developer(name);
        this.dataAccess.EditDeveloper(developer, oldName);
    }

    public void DeleteGame(string name)
    {
        this.dataAccess.DeleteGame(name);
    }

    public void DeleteDeveloper(string name)
    {
        this.dataAccess.DeleteDeveloper(name);
    }

    public abstract List<Game> GetGames();

    public abstract List<Developer> GetDevelopers();

    public List<Game> SortGamesList(List<Game> list, string order)
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

    public List<Developer> SortDevelopersList(List<Developer> list, string order)
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