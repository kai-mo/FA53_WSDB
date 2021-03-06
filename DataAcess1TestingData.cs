﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace GamesManager
{
    static class DataAcess1TestingData
    {
        static public void InsertTestingData()
        {
            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();
                if (CheckIfTableIsEmpty("games") || CheckIfTableIsEmpty("developers"))
                {
                    if (CheckIfTableIsEmpty("games"))
                    {
                        List<string> gameCommands = GetTestingGamesCommands();
                        foreach (var gameCommand in gameCommands)
                        {
                            using (SQLiteCommand command = new SQLiteCommand())
                            {
                                command.CommandText = gameCommand;
                                command.Connection = databaseConnection;
                                command.ExecuteNonQuery();
                            }
                        }
                    }

                    if (CheckIfTableIsEmpty("developers"))
                    {
                        List<string> developerCommands = GetTestingDevelopersCommands();
                        foreach (var developerCommand in developerCommands)
                        {
                            using (SQLiteCommand command = new SQLiteCommand())
                            {
                                command.CommandText = developerCommand;
                                command.Connection = databaseConnection;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }

        static private bool CheckIfTableIsEmpty(string tableName)
        {
            int count = 0;
            using (SQLiteConnection databaseConnection = GetDatabaseConnection())
            {
                databaseConnection.Open();
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    // TODO implement a solution that is more secure
                    command.CommandText = "SELECT COUNT(*) FROM " + Convert.ToString(tableName);
                    command.Connection = databaseConnection;
                    count = Convert.ToInt32(command.ExecuteScalar());
                    return (count == 0);
                }
            }
        }

        static private List<string> GetTestingGamesCommands()
        {
            List<string> commands = new List<string>()
            {
                "INSERT INTO games (name, developer) VALUES ('The Witcher 1', 1)",
                "INSERT INTO games(name, developer) VALUES('The Witcher 2', 1)",
                "INSERT INTO games(name, developer) VALUES('The Witcher 3', 1)",
                "INSERT INTO games(name, developer) VALUES('The Last of US', 2)",
                "INSERT INTO games(name, developer) VALUES('Uncharted 1', 2)",
                "INSERT INTO games(name, developer) VALUES('Uncharted 2', 2)",
                "INSERT INTO games(name, developer) VALUES('Uncharted 3', 2)",
                "INSERT INTO games(name, developer) VALUES('Uncharted 4', 2)",
                "INSERT INTO games(name, developer) VALUES('Titan Quest', 5)",
                "INSERT INTO games(name, developer) VALUES('StarCraft 2', 3)",
                "INSERT INTO games(name, developer) VALUES('Batman: Arkham Asylum', 4)",
                "INSERT INTO games(name, developer) VALUES('Batman: Arkham Knight', 4)"
            };
            return commands;
        }

        static private List<string> GetTestingDevelopersCommands()
        {
            List<string> commands = new List<string>()
            {
                "INSERT INTO developers (name) VALUES ('CD Project Red')",
                "INSERT INTO developers(name) VALUES('Naughty Dog')",
                "INSERT INTO developers(name) VALUES('Blizzard')",
                "INSERT INTO developers(name) VALUES('Rocksteady Studios');",
                "INSERT INTO developers(name) VALUES('Iron Lore Entertainment');"
            };
            return commands;
        }

        static private SQLiteConnection GetDatabaseConnection()
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
    }
}