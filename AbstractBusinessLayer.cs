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
        if (name.Equals(""))
        {
            throw new Exception("Game name cannot be an empty string.");
        }
        if (developerName.Equals(""))
        {
            throw new Exception("Developer name cannot be an empty string.");
        }
        this.dataAccess.AddGame(name, developerName);
    }

    public void AddDeveloper(string name)
    {
        if (name.Equals(""))
        {
            throw new Exception("Developer name cannot be an empty string.");
        }
        this.dataAccess.AddDeveloper(name);
    }

    public void EditGame(string newName, string oldName)
    {
        if (newName.Equals(""))
        {
            throw new Exception("Game name cannot be an empty string.");
        }
        if (oldName.Equals(""))
        {
            throw new Exception("Old game name cannot be an empty string.");
        }
        this.dataAccess.EditGame(newName, oldName);
    }

    public void EditDeveloper(string newName, string oldName)
    {

        if (newName.Equals(""))
        {
            throw new Exception("Developer name cannot be an empty string.");
        }
        if (oldName.Equals(""))
        {
            throw new Exception("Old developer name cannot be an empty string.");
        }
        this.dataAccess.EditDeveloper(newName, oldName);
    }

    public void DeleteGame(string name)
    {
        if (name.Equals(""))
        {
            throw new Exception("Game name cannot be an empty string.");
        }
        this.dataAccess.DeleteGame(name);
    }

    public void DeleteDeveloper(string name, bool force)
    {
        if (name.Equals(""))
        {
            throw new Exception("Developer name cannot be an empty string.");
        }
        Developer developer = dataAccess.GetDeveloper(name);
        if (!force && developer.Games.Count > 0)
        {
            throw new Exception("Developer still as games assigned.\nUse the force option to delete the developer and the assigned games.");
        }
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