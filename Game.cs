public class Game
{
    private int id;
    private string name;
    private Developer developer;

    public Game(int id, string name, Developer developer)
    {
        this.id = id;
        this.name = name;
        this.developer = developer;
    }

    public Game(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public Game(string name)
    {
        this.name = name;
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

    public Developer Developer
    {
        get { return this.developer; }
        set { this.developer = value; }
    }
}