using System;
using System.Drawing;
using System.Windows.Forms;

public class GUI : Form
{
    private IBusinessLayer businesslayer;

    private Button btnAddDeveloper;

    private GroupBox gbxGames;
    private GroupBox gbxDevelopers;

    private ListBox lbxGames;
    private ListBox lbxDevelopers;

    private TextBox tbxAddDeveloper;

    public GUI(IBusinessLayer businesslayer)
    {
        this.businesslayer = businesslayer;
        this.FormBorderStyle = FormBorderStyle.Fixed3D;
        this.MaximizeBox = false;
        this.Size = new Size(545, 500);
        this.Text = "Games Manager";

        InitializeComponents();

        LoadGames();
        LoadDevelopers();
    }

    private void InitializeComponents()
    {
        btnAddDeveloper = new Button();
        btnAddDeveloper.Location = new Point(270, 400);
        btnAddDeveloper.Size = new Size(250, 30);
        btnAddDeveloper.Text = "Add Developer";
        btnAddDeveloper.Click += new System.EventHandler(this.btnAddDeveloper_Click);
        Controls.Add(btnAddDeveloper);

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

        tbxAddDeveloper = new TextBox();
        tbxAddDeveloper.Location = new Point(270, 370);
        tbxAddDeveloper.Size = new Size(250, 30);
        Controls.Add(tbxAddDeveloper);
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

    private void ShowErrorMessageBox(string message)
    {
        MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void btnAddDeveloper_Click(object sender, System.EventArgs e)
    {
        try
        {
            businesslayer.AddDeveloper(tbxAddDeveloper.Text);
        }
        catch (Exception ex)
        {
            ShowErrorMessageBox(ex.Message);
            return;
        }

        tbxAddDeveloper.Clear();
        LoadDevelopers();
    }
}
