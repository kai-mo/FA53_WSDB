using System;
using System.Collections.Generic;

public class BusinessLayer2 : AbstractBusinessLayer
{
    public BusinessLayer2(IDataAccess dataAccess) : base(dataAccess)
    { }

    public override List<Game> getGames()
    {
        var games = this.dataAccess.getGames();
        return this.sortGamesList(games, "DESC");
    }

    public override List<Developer> getDevelopers()
    {
        var developers = this.dataAccess.getDevelopers();
        return this.sortDevelopersList(developers, "DESC");
    }
}
