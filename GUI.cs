using System;
using System.Drawing;
using System.Windows.Forms;

public class GUI : Form
{
    private IBusinessLayer businessLayer;

    private Button btnAddDeveloper;
    private Button btnAddGame;

    private ComboBox cbxDevelopers;
    private const string CBX_DEVELOPERS_DEFAULT_TEXT = "Choose Developer";

    private ContextMenu ctmDevelopers;
    private ContextMenu ctmGames;

    private GroupBox gbxGames;
    private GroupBox gbxDevelopers;

    private Label lblAddDeveloper;
    private Label lblAddGame;

    private ListBox lbxGames;
    private ListBox lbxDevelopers;

    private TextBox tbxAddDeveloper;
    private TextBox tbxAddGame;

    private TreeView tvwAssignments;

    public GUI(IBusinessLayer businessLayer)
    {
        this.businessLayer = businessLayer;
        this.FormBorderStyle = FormBorderStyle.Fixed3D;
        this.MaximizeBox = false;
        this.Size = new Size(545, 730);
        this.Text = "Games Manager";
        this.StartPosition = FormStartPosition.CenterScreen;

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

        ctmDevelopers = new ContextMenu();
        MenuItem mniDeleteDeveloper = new MenuItem("Delete");
        MenuItem mniEditDeveloper = new MenuItem("Edit");
        mniDeleteDeveloper.Click += new System.EventHandler(this.deleteDeveloper_Click);
        mniEditDeveloper.Click += new System.EventHandler(this.editDeveloper_Click);
        ctmDevelopers.MenuItems.Add(mniDeleteDeveloper);
        ctmDevelopers.MenuItems.Add(mniEditDeveloper);

        ctmGames = new ContextMenu();
        MenuItem mniDeleteGame = new MenuItem("Delete");
        MenuItem mniEditGame = new MenuItem("Edit");
        mniDeleteGame.Click += new System.EventHandler(this.deleteGame_Click);
        mniEditGame.Click += new System.EventHandler(this.editGame_Click);
        ctmGames.MenuItems.Add(mniDeleteGame);
        ctmGames.MenuItems.Add(mniEditGame);


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

        lblAddDeveloper = new Label();
        lblAddDeveloper.AutoSize = true;
        lblAddDeveloper.Location = new Point(270, 370);
        lblAddDeveloper.Text = "Name:";
        Controls.Add(lblAddDeveloper);

        lblAddGame = new Label();
        lblAddGame.AutoSize = true;
        lblAddGame.Location = new Point(10, 370);
        lblAddGame.Text = "Name:";
        Controls.Add(lblAddGame);

        lbxGames = new ListBox();
        lbxGames.Dock = DockStyle.Fill;
        lbxGames.ContextMenu = ctmGames;
        gbxGames.Controls.Add(lbxGames);

        lbxDevelopers = new ListBox();
        lbxDevelopers.Dock = DockStyle.Fill;
        lbxDevelopers.ContextMenu = ctmDevelopers;
        gbxDevelopers.Controls.Add(lbxDevelopers);

        tbxAddDeveloper = new TextBox();
        tbxAddDeveloper.Location = new Point(310, 370);
        tbxAddDeveloper.Size = new Size(210, 30);
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

        foreach(Developer developer in businessLayer.GetDevelopers())
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

        foreach (Game game in businessLayer.GetGames())
        {
            lbxGames.Items.Add(game.Name);
        }
    }

    private void LoadDevelopers()
    {
        lbxDevelopers.Items.Clear();
        cbxDevelopers.Items.Clear();

        foreach (Developer developer in businessLayer.GetDevelopers())
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
            businessLayer.AddDeveloper(tbxAddDeveloper.Text);
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
            businessLayer.AddGame(tbxAddGame.Text, cbxDevelopers.Text);
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

    private void deleteDeveloper_Click(object sender, System.EventArgs e) {
        if (lbxDevelopers.SelectedIndex == -1) {
            return;
        }

        businessLayer.DeleteDeveloper(lbxDevelopers.SelectedItem.ToString(), true);
        LoadAssignments();
        LoadDevelopers();
        LoadGames();
    }

    private void editDeveloper_Click(object sender, System.EventArgs e) {
        if (lbxDevelopers.SelectedIndex == -1) {
            return;
        }

        string oldName = lbxDevelopers.SelectedItem.ToString();
        EditDialog f = new EditDialog(oldName);
        f.StartPosition = FormStartPosition.CenterScreen;

        if (f.ShowDialog() == DialogResult.OK) {
            try {
                businessLayer.EditDeveloper(f.NewName, oldName);
            } catch(Exception ex) {
                ShowErrorMessageBox(ex.Message);
                return;
            }
            LoadAssignments();
            LoadDevelopers();
            LoadGames();
        }
    }

    private void deleteGame_Click(object sender, System.EventArgs e) {
        if (lbxGames.SelectedIndex == -1) {
            return;
        }

        businessLayer.DeleteGame(lbxGames.SelectedItem.ToString());
        LoadAssignments();
        LoadGames();
    }

    private void editGame_Click(object sender, System.EventArgs e) {
        if ( lbxGames.SelectedIndex == -1) {
            return;
        }

        string oldName = lbxGames.SelectedItem.ToString();
        EditDialog f = new EditDialog(oldName);
        f.StartPosition = FormStartPosition.CenterScreen;

        if (f.ShowDialog() == DialogResult.OK) {
            try {
                businessLayer.EditGame(f.NewName, oldName);
            } catch(Exception ex) {
                ShowErrorMessageBox(ex.Message);
                return;
            }
            LoadGames();
            LoadAssignments();
        }
    }

}
