using System.Collections.Generic;

namespace GamesManager
{
    public class BusinessLayer1 : AbstractBusinessLayer
    {
        public BusinessLayer1(IDataAccess dataAccess) : base(dataAccess)
        { }

        public override List<Game> GetGames()
        {
            var games = this.dataAccess.GetGames();
            return this.SortGamesList(games, "ASC");
        }

        public override List<Developer> GetDevelopers()
        {
            var developers = this.dataAccess.GetDevelopers();
            return this.SortDevelopersList(developers, "ASC");
        }
    }
}