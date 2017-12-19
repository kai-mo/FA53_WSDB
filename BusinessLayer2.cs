using System.Collections.Generic;

namespace GamesManager
{
    public class BusinessLayer2 : AbstractBusinessLayer
    {
        public BusinessLayer2(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public override List<Game> GetGames()
        {
            var games = this.dataAccess.GetGames();
            return this.SortGamesList(games, "DESC");
        }

        public override List<Developer> GetDevelopers()
        {
            var developers = this.dataAccess.GetDevelopers();
            return this.SortDevelopersList(developers, "DESC");
        }
    }
}
