using System;
using System.Drawing;
using System.Windows.Forms;

public class GUI : Form
{
    private IBusinessLayer businesslayer;

    private Button btnAddDeveloper;
    private Button btnAddGame;

    private ComboBox cbxDevelopers;
    private const string CBX_DEVELOPERS_DEFAULT_TEXT = "Choose Developer";

    private GroupBox gbxGames;
    private GroupBox gbxDevelopers;

    private Label lblAddGame;

    private ListBox lbxGames;
    private ListBox lbxDevelopers;

    private TextBox tbxAddDeveloper;
    private TextBox tbxAddGame;

    private TreeView tvwAssignments;

    public GUI(IBusinessLayer businesslayer)
    {
        this.businesslayer = businesslayer;
        this.FormBorderStyle = FormBorderStyle.Fixed3D;
        this.MaximizeBox = false;
        this.Size = new Size(545, 730);
        this.Text = "Games Manager";

        InitializeComponents();

        LoadAssignments();
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

        btnAddGame = new Button();
        btnAddGame.Location = new Point(10, 430);
        btnAddGame.Size = new Size(250, 30);
        btnAddGame.Text = "Add Game";
        btnAddGame.Click += new System.EventHandler(this.btnAddGame_Click);
        Controls.Add(btnAddGame);

        cbxDevelopers = new ComboBox();
        cbxDevelopers.Location = new Point(10, 400);
        cbxDevelopers.Size = new Size(250, 30);
        cbxDevelopers.Text = CBX_DEVELOPERS_DEFAULT_TEXT;
        Controls.Add(cbxDevelopers);

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

        lblAddGame = new Label();
        lblAddGame.AutoSize = true;
        lblAddGame.Location = new Point(10, 370);
        lblAddGame.Text = "Name:";
        Controls.Add(lblAddGame);

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

        tbxAddGame = new TextBox();
        tbxAddGame.Location = new Point(50, 370);
        tbxAddGame.Size = new Size(210, 30);
        Controls.Add(tbxAddGame);

        tvwAssignments = new TreeView();
        tvwAssignments.Location = new Point(10, 470);
        tvwAssignments.Size = new Size(510, 210);
        Controls.Add(tvwAssignments);
    }

    private void LoadAssignments()
    {
        tvwAssignments.Nodes.Clear();

        foreach(Developer developer in businesslayer.GetDevelopers())
        {
            TreeNode curr = new TreeNode(developer.Name);
            tvwAssignments.Nodes.Add(curr);

            foreach(Game game in developer.Games)
            {
                curr.Nodes.Add(new TreeNode(game.Name));
            }
        }
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
        cbxDevelopers.Items.Clear();

        foreach (Developer developer in businesslayer.GetDevelopers())
        {
            lbxDevelopers.Items.Add(developer.Name);
            cbxDevelopers.Items.Add(developer.Name);
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
        LoadAssignments();
        LoadDevelopers();
    }

    private void btnAddGame_Click(object sender, System.EventArgs e)
    {
        if (cbxDevelopers.SelectedIndex == -1)
        {
            ShowErrorMessageBox("Please choose a developer!");
            return;
        }

        try
        {
            businesslayer.AddGame(tbxAddGame.Text, cbxDevelopers.Text);
        }
        catch (Exception ex)
        {
            ShowErrorMessageBox(ex.Message);
            return;
        }

        tbxAddGame.Clear();
        cbxDevelopers.SelectedIndex = -1;
        cbxDevelopers.Text = CBX_DEVELOPERS_DEFAULT_TEXT;

        LoadAssignments();
        LoadGames();
    }
}
