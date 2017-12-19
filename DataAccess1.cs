using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace GamesManager
{
    public class DataAccess1 : IDataAccess
    {
        public DataAccess1()
        {
            VerifyDatabaseStructur();
            DataAcess1TestingData.InsertTestingData();
        }

        private SQLiteConnection GetDatabaseConnection()
        {
            string fileName = @"GamesManager.sqlite";
            SQLiteConnection.ClearAllPools();

            if (!File.Exists(fileName))
            {
                SQLiteConnection.CreateFile(fileName);
            }

            SQLiteConnection databaseConnection = new SQLiteConnection("Data Source=GamesManager.sqlite;Version=3;");

            return databaseConnection;
        }

        private void VerifyDatabaseStructur()
        {
            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS games (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(255) NOT NULL DEFAULT '', developer INTEGER NOT NULL DEFAULT 0)";
                    command.Connection = databaseConnection;
                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS developers (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(255) NOT NULL DEFAULT '')";
                    command.Connection = databaseConnection;
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool AddGame(string name, string developerName)
        {
            int rowsAffected = 0;

            if (CheckIfGameExists(name))
            {
                throw new Exception("A game with this name already exists");
            }

            if (!CheckIfDeveloperExists(developerName))
            {
                AddDeveloper(developerName);
            }

            Developer developer = GetDeveloper(developerName);
            int developerId = developer.Id;

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "INSERT INTO games(name, developer) VALUES(@name, @developerId)";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@developerId", developerId);
                    rowsAffected += command.ExecuteNonQuery();
                }
            }

            return (rowsAffected > 0);
        }

        public bool AddDeveloper(string name)
        {
            int rowsAffected = 0;

            if (CheckIfDeveloperExists(name))
            {
                throw new Exception("A developer with this name already exists");
            }

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "INSERT INTO developers(name) VALUES(@name)";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@name", name);
                    rowsAffected += command.ExecuteNonQuery();
                }
            }

            return (rowsAffected > 0);
        }

        public bool EditGame(string newName, string oldName)
        {
            int rowsAffected = 0;

            if (CheckIfGameExists(newName))
            {
                throw new Exception("A game with this name already exists");
            }

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "UPDATE games SET name = @newName WHERE name = @oldName";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@newName", newName);
                    command.Parameters.AddWithValue("@oldName", oldName);
                    rowsAffected += command.ExecuteNonQuery();
                }
            }

            return (rowsAffected > 0);
        }

        public bool EditDeveloper(string newName, string oldName)
        {
            int rowsAffected = 0;

            if (CheckIfDeveloperExists(newName))
            {
                throw new Exception("A developer with this name already exists");
            }

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "UPDATE developers SET name = @newName WHERE name = @oldName";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@newName", newName);
                    command.Parameters.AddWithValue("@oldName", oldName);
                    rowsAffected += command.ExecuteNonQuery();
                }
            }

            return (rowsAffected > 0);
        }

        public bool DeleteGame(string name)
        {
            int rowsAffected = 0;

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "DELETE FROM games WHERE name = @name";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@name", name);
                    rowsAffected += command.ExecuteNonQuery();
                }
            }

            return (rowsAffected > 0);
        }

        public bool DeleteDeveloper(string name)
        {
            int rowsAffected = 0;
            DeleteDeveloperGames(name);

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "DELETE FROM developers WHERE name = @name";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@name", name);
                    rowsAffected += command.ExecuteNonQuery();
                }
            }

            return (rowsAffected > 0);
        }

        public Developer GetDeveloper(int id)
        {
            Developer developer;

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "SELECT * FROM developers WHERE id = @id";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@id", id);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["id"]);
                            string name = Convert.ToString(reader["name"]);
                            developer = new Developer(id, name);

                            List<Game> games = GetDeveloperGames(id);

                            if (games.Count > 0)
                            {
                                developer.Games = games;
                            }

                            return new Developer(id, name);
                        }

                        throw new Exception("Developer not found");
                    }
                }
            }
        }

        public Developer GetDeveloper(string name)
        {
            Developer developer;

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "SELECT * FROM developers WHERE name = @name";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@name", name);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            name = Convert.ToString(reader["name"]);
                            developer = new Developer(id, name);

                            List<Game> games = GetDeveloperGames(id);

                            if (games.Count > 0)
                            {
                                developer.Games = games;
                            }

                            return new Developer(id, name);
                        }

                        throw new Exception("Developer not found");
                    }
                }
            }
        }

        public List<Game> GetGames()
        {
            List<Game> games = new List<Game>();

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "SELECT * FROM games";
                    command.Connection = databaseConnection;

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string name = Convert.ToString(reader["name"]);
                            Game game = new Game(id, name);
                            games.Add(game);
                        }
                    }
                }
            }

            return games;
        }

        public List<Developer> GetDevelopers()
        {
            List<Developer> developers = new List<Developer>();

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "SELECT * FROM developers";
                    command.Connection = databaseConnection;

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string name = Convert.ToString(reader["name"]);
                            Developer developer = new Developer(id, name);
                            List<Game> games = GetDeveloperGames(id);
                            developer.Games = games;
                            developers.Add(developer);
                        }
                    }
                }
            }

            return developers;
        }

        private bool DeleteDeveloperGames(string name)
        {
            int rowsAffected = 0;

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "DELETE FROM games WHERE developer = (SELECT id FROM developers WHERE name = @name)";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@name", name);
                    rowsAffected += command.ExecuteNonQuery();
                }
            }

            return (rowsAffected > 0);
        }

        private bool CheckIfGameExists(string name)
        {
            int count = 0;

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM games WHERE name = @name";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@name", name);
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return (count > 0);
        }

        private bool CheckIfDeveloperExists(string name)
        {
            int count = 0;

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM developers WHERE name = @name";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@name", name);
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return (count > 0);
        }

        private List<Game> GetDeveloperGames(int developerId)
        {
            List<Game> games = new List<Game>();

            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();

                using (SQLiteCommand command = new SQLiteCommand())
                {
                    command.CommandText = "SELECT * FROM games WHERE developer = @developerId";
                    command.Connection = databaseConnection;
                    command.Parameters.AddWithValue("@developerId", developerId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string name = Convert.ToString(reader["name"]);
                            Game game = new Game(id, name);
                            games.Add(game);
                        }
                    }
                }
            }

            return games;
        }
    }
}
