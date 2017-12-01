using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

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
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "CREATE TABLE IF NOT EXISTS games (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(255) NOT NULL DEFAULT '', developer INTEGER NOT NULL DEFAULT 0)",
                Connection = databaseConnection

            };
            command.ExecuteNonQuery();

            command = new SQLiteCommand
            {
                CommandText = "CREATE TABLE  IF NOT EXISTS developers (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(255) NOT NULL DEFAULT '')",
                Connection = databaseConnection
            };
            command.ExecuteNonQuery();
            databaseConnection.Close();
        }

    }

    public bool AddGame(string name, string developerName)
    {
        if (name == "")
        {
            throw new Exception("Game name cannot be an empty string.");
        }
        if (developerName == "")
        {
            throw new Exception("Developer name cannot be an empty string.");
        }
        if (!CheckIfDeveloperExists(developerName))
        {
            AddDeveloper(developerName);
        }
        Developer developer = GetDeveloper(developerName);
        int developerId = developer.Id;
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            int rowsAffected = 0;
            databaseConnection.Open();
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "INSERT INTO games(name, developer) VALUES(@name, @developerId)",
                Connection = databaseConnection
            };
            SQLiteParameter nameParam = new SQLiteParameter("@name", name);
            SQLiteParameter developerIdParam = new SQLiteParameter("@developerId", developerId);
            command.Parameters.Add(nameParam);
            command.Parameters.Add(developerIdParam);
            command.Prepare();
            rowsAffected += command.ExecuteNonQuery();
            databaseConnection.Close();
            return (rowsAffected > 0);
        }
    }

    public bool AddDeveloper(string name)
    {
        if (name == "")
        {
            throw new Exception("Developer name cannot be an empty string.");
        }
        if (CheckIfDeveloperExists(name))
        {
            throw new Exception("Developer already exisits");
        }
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            int rowsAffected = 0;
            databaseConnection.Open();
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "INSERT INTO developers(name) VALUES(@name)",
                Connection = databaseConnection
            };
            SQLiteParameter nameParam = new SQLiteParameter("@name", name);
            command.Parameters.Add(nameParam);
            command.Prepare();
            rowsAffected += command.ExecuteNonQuery();
            databaseConnection.Close();
            return (rowsAffected > 0);
        }
    }

    public bool EditGame(string newName, string oldName)
    {
        if (newName == "")
        {
            throw new Exception("Game name cannot be an empty string.");
        }
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            int rowsAffected = 0;
            databaseConnection.Open();
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "UPDATE games SET name = @newName WHERE name = @oldName",
                Connection = databaseConnection
            };
            SQLiteParameter newNameParam = new SQLiteParameter("@newName", newName);
            SQLiteParameter oldNameParam = new SQLiteParameter("@oldName", oldName);
            command.Parameters.Add(newNameParam);
            command.Parameters.Add(oldNameParam);
            command.Prepare();
            rowsAffected += command.ExecuteNonQuery();
            databaseConnection.Close();
            return (rowsAffected > 0);
        }
    }

    public bool EditDeveloper(string newName, string oldName)
    {
        if (newName == "")
        {
            throw new Exception("Developer name cannot be an empty string.");
        }
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            int rowsAffected = 0;
            databaseConnection.Open();
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "UPDATE developers SET name = @newName WHERE name = @oldName",
                Connection = databaseConnection
            };
            SQLiteParameter newNameParam = new SQLiteParameter("@newName", newName);
            SQLiteParameter oldNameParam = new SQLiteParameter("@oldName", oldName);
            command.Parameters.Add(newNameParam);
            command.Parameters.Add(oldNameParam);
            command.Prepare();
            rowsAffected += command.ExecuteNonQuery();
            databaseConnection.Close();
            return (rowsAffected > 0);
        }
    }

    public bool DeleteGame(string name)
    {
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            int rowsAffected = 0;
            databaseConnection.Open();
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "DELETE FROM games WHERE name = @name",
                Connection = databaseConnection
            };
            SQLiteParameter nameParam = new SQLiteParameter("@name", name);
            command.Parameters.Add(nameParam);
            command.Prepare();
            rowsAffected += command.ExecuteNonQuery();
            databaseConnection.Close();
            return (rowsAffected > 0);
        }
    }

    public bool DeleteDeveloper(string name)
    {
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            DeleteDeveloperGames(name);
            int rowsAffected = 0;
            databaseConnection.Open();
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "DELETE FROM developers WHERE name = @name",
                Connection = databaseConnection
            };
            SQLiteParameter nameParam = new SQLiteParameter("@name", name);
            command.Parameters.Add(nameParam);
            command.Prepare();
            rowsAffected += command.ExecuteNonQuery();
            databaseConnection.Close();
            return (rowsAffected > 0);
        }
    }

    private bool DeleteDeveloperGames(string name)
    {
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            int rowsAffected = 0;
            databaseConnection.Open();
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "DELETE FROM games WHERE developer = (SELECT id FROM developers WHERE name = @name)",
                Connection = databaseConnection
            };
            SQLiteParameter nameParam = new SQLiteParameter("@name", name);
            command.Parameters.Add(nameParam);
            command.Prepare();
            rowsAffected += command.ExecuteNonQuery();
            databaseConnection.Close();
            return (rowsAffected > 0);
        }
    }

    public List<Game> GetGames()
    {
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            List<Game> games = new List<Game>();
            databaseConnection.Open();

            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "SELECT * FROM games",
                Connection = databaseConnection
            };
            command.Prepare();
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    Game game = new Game(id, name);
                    games.Add(game);
                }
            }
            reader.Close();

            databaseConnection.Close();
            return games;
        }
    }

    public List<Developer> GetDevelopers()
    {
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            databaseConnection.Open();
            List<Developer> developers = new List<Developer>();
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "SELECT * FROM developers",
                Connection = databaseConnection
            };
            command.Prepare();
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"]);
                string name = Convert.ToString(reader["name"]);
                Developer developer = new Developer(id, name);
                List<Game> games = GetDeveloperGames(id);
                developer.Games = games;
                developers.Add(developer);
            }
            databaseConnection.Close();
            return developers;
        }
    }

    private bool CheckIfDeveloperExists(string name)
    {
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            databaseConnection.Open();
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "SELECT COUNT(*) FROM developers WHERE name = @name",
                Connection = databaseConnection
            };
            SQLiteParameter nameParam = new SQLiteParameter("@name", name);
            command.Parameters.Add(nameParam);
            command.Prepare();
            int count = Convert.ToInt32(command.ExecuteScalar());
            databaseConnection.Close();
            return (count > 0);
        }
    }

    private List<Game> GetDeveloperGames(int developerId)
    {
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            databaseConnection.Open();
            List<Game> games = new List<Game>();
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "SELECT * FROM games WHERE developer = @developerId",
                Connection = databaseConnection
            };
            SQLiteParameter idParam = new SQLiteParameter("@developerId", developerId);
            command.Parameters.Add(idParam);
            command.Prepare();
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    Game game = new Game(id, name);
                    games.Add(game);
                }
            }
            reader.Close();
            databaseConnection.Close();
            return games;
        }
    }

    private Developer GetDeveloper(int id)
    {
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            databaseConnection.Open();
            Developer developer;
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "SELECT * FROM developers WHERE id = @id",
                Connection = databaseConnection
            };
            SQLiteParameter idParam = new SQLiteParameter("@id", id);
            command.Parameters.Add(idParam);
            command.Prepare();
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    developer = new Developer(id, name);
                    reader.Close();
                    databaseConnection.Close();

                    List<Game> games = GetDeveloperGames(id);
                    if (games.Count > 0)
                    {
                        developer.Games = games;
                    }
                    return new Developer(id, name);
                }
                throw new SQLiteException("Developer not found");
            }
            else
            {
                reader.Close();
                databaseConnection.Close();
                throw new SQLiteException("Developer not found");
            }
        }
    }

    private Developer GetDeveloper(string name)
    {
        using (SQLiteConnection databaseConnection = GetDatabaseConnection())
        {
            databaseConnection.Open();
            Developer developer;
            SQLiteCommand command = new SQLiteCommand
            {
                CommandText = "SELECT * FROM developers WHERE name = @name",
                Connection = databaseConnection
            };
            SQLiteParameter nameParam = new SQLiteParameter("@name", name);
            command.Parameters.Add(nameParam);
            command.Prepare();
            SQLiteDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    name = Convert.ToString(reader["name"]);
                    developer = new Developer(id, name);
                    reader.Close();
                    databaseConnection.Close();

                    List<Game> games = GetDeveloperGames(id);
                    if (games.Count > 0)
                    {
                        developer.Games = games;
                    }
                    return new Developer(id, name);
                }
                throw new SQLiteException("Developer not found");
            }
            else
            {
                reader.Close();
                databaseConnection.Close();
                throw new SQLiteException("Developer not found");
            }
        }
    }
}
