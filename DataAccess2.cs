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
        Developer developer = null;
        foreach (var existingDeveloper in this.developers)
        {
            if (developerName.Equals(existingDeveloper.Name))
            {
                developer = existingDeveloper;
            }
            foreach (var game in existingDeveloper.Games)
            {
                if (game.Name.Equals(name))
                {
                    throw new Exception("A game with this name already exists");
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
        Developer developer = new Developer(name);
        if (this.developers.Find(item => item.Name.Equals(name)) == null)
        {
            this.developers.Add(developer);
            WriteToJsonFile();
            return true;
        }
        throw new Exception("A developer with this name already exists");
    }

    public bool EditGame(string newName, string oldName)
    {
        foreach (var existingDeveloper in this.developers)
        {
            int index = 0;
            foreach (var developerGame in existingDeveloper.Games)
            {
                if (developerGame.Name.Equals(newName))
                {
                    throw new Exception("A game with this name already exists");
                }
                if (developerGame.Name.Equals(oldName))
                {
                    existingDeveloper.Games[index].Name = newName;
                    WriteToJsonFile();
                    return true;
                }
                index++;
            }
        }
        return false;
    }

    public bool EditDeveloper(string newName, string oldName)
    {
        if (this.developers.Find(item => item.Name.Equals(newName)) != null)
        {
            throw new Exception("A developer with this name already exists");
        }
        Developer developerToEdit = this.developers.Find(item => item.Name.Equals(oldName));
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
                        if (oldName.Equals(developerGame.Name))
                        {
                            developerGame.Name = newName;
                        }
                    }
                }
            }

            WriteToJsonFile();
            return true;
        }
        return false;
    }

    public bool DeleteGame(string name)
    {
        foreach (var existingDeveloper in this.developers)
        {
            int index = 0;
            foreach (var developerGame in existingDeveloper.Games)
            {
                if (name.Equals(developerGame.Name))
                {
                    existingDeveloper.Games.RemoveAt(index);
                    WriteToJsonFile();
                    return true;
                }
                index++;
            }
        }
        return false;
    }

    public bool DeleteDeveloper(string name)
    {
        Developer developer = developers.Find(item => item.Name.Equals(name));
        if (developer != null)
        {
            developers.Remove(developer);
            WriteToJsonFile();
            return true;
        }
        return false;
    }

    public Developer GetDeveloper(string name)
    {
        Developer developer = this.developers.Find(item => item.Name.Equals(name));
        if (developer == null)
        {
            throw new Exception("Developer not found");
        }
        return developer;
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
        foreach (Developer developer in this.developers)
        {
            if (developer.Games.Equals(null))
            {
                developer.Games = new List<Game>();
            }
        }
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
        using (StreamWriter streamWriter = new StreamWriter(this.filePath))
        {
            streamWriter.Write(json);
        }
    }

    public List<Developer> ReadFromJsonFile()
    {
        List<Developer> developers = new List<Developer>();
        if (!File.Exists(this.filePath))
        {
            using (File.Create(this.filePath)) { };
        }
        using (StreamReader streamReader = new StreamReader(this.filePath))
        {
            string json = streamReader.ReadToEnd();
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
                throw new FileLoadException("Json seems not to be valid");
            }
        }
        return developers;
    }
}