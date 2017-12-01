using System;
using System.Drawing;
using System.Windows.Forms;

public class GUI : Form
{
    private IBusinessLayer businesslayer;

    private GroupBox gbxGames;
    private GroupBox gbxDevelopers;

    private ListBox lbxGames;
    private ListBox lbxDevelopers;

    public GUI(IBusinessLayer businesslayer)
    {
        this.businesslayer = businesslayer;
        this.Text = "Games Manager";
        this.Size = new Size(545, 500);

        gbxGames = new GroupBox();
        gbxGames.Text = "Games";
        gbxGames.Location = new Point(10, 10);
        gbxGames.Size = new Size(250, 350);
        Controls.Add(gbxGames);

        gbxDevelopers = new GroupBox();
        gbxDevelopers.Text = "Developers";
        gbxDevelopers.Location = new Point(270, 10);
        gbxDevelopers.Size = new Size(250, 350);
        Controls.Add(gbxDevelopers);

        lbxGames = new ListBox();
        lbxGames.Dock = DockStyle.Fill;
        gbxGames.Controls.Add(lbxGames);

        lbxDevelopers = new ListBox();
        lbxDevelopers.Dock = DockStyle.Fill;
        gbxDevelopers.Controls.Add(lbxDevelopers);

        LoadGames();
        LoadDevelopers();
    }

    private void LoadGames()
    {
        lbxGames.Items.Clear();

        foreach (Game game in businesslayer.GetGames())
        {
            lbxGames.Items.Add(game.Name);
        }
    }

    private void LoadDevelopers()
    {
        lbxDevelopers.Items.Clear();

        foreach (Developer developer in businesslayer.GetDevelopers())
        {
            lbxDevelopers.Items.Add(developer.Name);
        }
    }
}
