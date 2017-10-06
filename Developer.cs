public class Developer
{
    private int id;
    private string name;
    private Game[] games;

	public Developer(int id, string name)
	{
        this.id = id;
        this.name = name;
	}

    public Developer(string name)
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

    public Game[] Games
    {
        get { return this.games; }
        set { this.games = value; }
    }
}
