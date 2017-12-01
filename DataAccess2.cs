using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

 // TODO check if some exceptions should be called in the business layer
 // TODO check if we can create a unique id for each record
public class DataAccess2 : IDataAccess
{
    private string filePath = @"GamesManager.json";
    private List<Developer> developers;

    public DataAccess2()
    {
        this.developers = ReadFromJsonFile();
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
        Developer developer = null;
        foreach (var existingDeveloper in this.developers)
        {
            if (developerName == existingDeveloper.Name)
            {
                developer = existingDeveloper;
            }
            foreach (var game in existingDeveloper.Games)
            {
                if (game.Name == name)
                {
                    throw new Exception(String.Format("Game already belongs to a developer: {0}", existingDeveloper.Name));
                }
            }
        }

        Game newGame = new Game(name);
        if (developer != null)
        {
            developer.Games.Add(newGame);
            WriteToJsonFile();
            return true;
        }
        this.developers.Add(new Developer(developerName, new List<Game> { newGame }));
        WriteToJsonFile();
        return true;
    }

    public bool AddDeveloper(string name)
    {
        if (name == "")
        {
            throw new Exception("Developer name cannot be an empty string.");
        }
        Developer developer = new Developer(name);
        if (this.developers.Find(item => item.Name == name) == null)
        {
            this.developers.Add(developer);
            WriteToJsonFile();
            return true;
        }
        return false;
    }

    public bool EditGame(string newName, string oldName)
    {
        foreach (var existingDeveloper in this.developers)
        {
            int index = 0;
            foreach (var developerGame in existingDeveloper.Games)
            {
                if (developerGame.Name == oldName)
                {
                    existingDeveloper.Games[index].Name = newName;
                    WriteToJsonFile();
                    return true;
                }
                index++;
            }
        }
        throw new Exception("Game not found.");
    }

    public bool EditDeveloper(string newName, string oldName)
    {
        Developer developerToEdit = this.developers.Find(item => item.Name == oldName);
        if (developerToEdit != null)
        {
            int index = developers.IndexOf(developerToEdit);
            developers[index].Name = newName;

            // change developer name for games with this developer
            foreach (var existingDeveloper in this.developers)
            {
                if (existingDeveloper.Games != null)
                {
                    foreach (var developerGame in existingDeveloper.Games)
                    {
                        if (oldName == developerGame.Name)
                        {
                            developerGame.Name = newName;
                        }
                    }
                }
            }

            WriteToJsonFile();
            return true;
        }
        throw new Exception("Developer not found.");
    }

    public bool DeleteGame(string name)
    {
        foreach (var existingDeveloper in this.developers)
        {
            int index = 0;
            foreach (var developerGame in existingDeveloper.Games)
            {
                if (name == developerGame.Name)
                {
                    existingDeveloper.Games.RemoveAt(index);
                    WriteToJsonFile();
                    return true;
                }
                index++;
            }
        }
        throw new Exception("Game not found.");
    }

    public bool DeleteDeveloper(string name)
    {
        Developer developer = developers.Find(item => item.Name == name);
        if (developer != null)
        {
            developers.Remove(developer);
            WriteToJsonFile();
            return true;
        }
        return false;
    }

    public List<Game> GetGames()
    {
        List<Game> games = new List<Game>();
        if (developers.Count > 0)
        {
            foreach (var developer in developers)
            {
                if (developer.Games.Count > 0)
                {
                    foreach (var game in developer.Games)
                    {
                        if (!games.Contains(game))
                        {
                            games.Add(game);
                        }
                    }
                }
            }
        }
        return games;
    }

    public List<Developer> GetDevelopers()
    {
        return this.developers;
    }

    public void WriteToJsonFile()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            // preservers reference loop
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            // save code indented for better testing
            Formatting = Formatting.Indented
        };
        string json = JsonConvert.SerializeObject(this.developers, settings);
        File.WriteAllText(this.filePath, json);
    }

    public List<Developer> ReadFromJsonFile()
    {
        List<Developer> developers = new List<Developer>();
        if (!File.Exists(this.filePath))
        {
            File.Create(this.filePath);
        }

        string json = File.ReadAllText(this.filePath);
        try
        {
            developers = JsonConvert.DeserializeObject<List<Developer>>(json);
            if (developers == null)
            {
                developers = new List<Developer>();
            }
        }
        catch (Exception)
        {
            throw new FileLoadException("Json seems not to be valid.");
        }

        return developers;
    }

    // TODO: Remove this legacy code later
    //private int GetNextpossibleDeveloperID()
    //{
    //    int nextPossibleID = 1;
    //    if (this.developers.Count > 0)
    //    {
    //        List<Developer> sortedList;
    //        sortedList = this.developers.OrderBy(item => item.ID).ToList();
    //        nextPossibleID = sortedList.Last().ID + 1;
    //    }
    //    return nextPossibleID;
    //}
}