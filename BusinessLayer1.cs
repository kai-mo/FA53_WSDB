using System;
using System.Collections.Generic;

public class BusinessLayer1 : AbstractBusinessLayer
{
    public BusinessLayer1(IDataAccess dataAccess) : base(dataAccess)
    { }

    public override List<Game> getGames()
    {
        var games = this.dataAccess.getGames();
        return this.sortGamesList(games, "ASC");
    }

    public override List<Developer> getDevelopers()
    {
        var developers = this.dataAccess.getDevelopers();
        return this.sortDevelopersList(developers, "ASC");
    }
}