using System.Collections.Generic;
using Newtonsoft.Json;

public class Developer
{
    private int id;
    private string name;
    private List<Game> games;

    public Developer(string name)
    {
        this.name = name;
    }

    public Developer(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    [JsonConstructor]
    public Developer(string name, List<Game> games)
    {
        this.name = name;
        this.games = games;
    }

    public Developer(int id, string name, List<Game> games)
    {
        this.id = id;
        this.name = name;
        this.games = games;
    }

    public int ID
    {
        get { return this.id; }
        set { this.id = value; }
    }

    public string Name
    {
        get { return this.name; }
        set { this.name = value; }
    }

    public List<Game> Games
    {
        get { return this.games; }
        set { this.games = value; }
    }
}