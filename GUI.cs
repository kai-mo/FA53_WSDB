using System;
using System.Drawing;
using System.Windows.Forms;

public class GUI : Form
{
    private IBusinessLayer businesslayer;
    private ListBox lbxGames;
    private GroupBox gbxGames;

    public GUI(IBusinessLayer businesslayer)
    {
        this.businesslayer = businesslayer;
        this.Text = "Games Manager";
        this.Size = new Size(500, 500);

        gbxGames = new GroupBox();
        gbxGames.Text = "Games";
        gbxGames.Location = new Point(10, 10);
        gbxGames.Size = new Size(250, 350);
        Controls.Add(gbxGames);

        lbxGames = new ListBox();
        lbxGames.Dock = DockStyle.Fill;
        gbxGames.Controls.Add(lbxGames);

        LoadGames();
    }

    private void LoadGames()
    {
        lbxGames.Items.Clear();

        foreach (Game game in businesslayer.GetGames())
        {
            lbxGames.Items.Add(game.Name);
        }
    }
}
