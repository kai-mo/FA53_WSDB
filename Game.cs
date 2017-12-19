using Newtonsoft.Json;

namespace GamesManager
{
    public class Game
    {
        private int id;
        private string name;

        [JsonConstructor]
        public Game(string name)
        {
            this.name = name;
        }

        public Game(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}