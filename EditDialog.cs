using System;
using System.Drawing;
using System.Windows.Forms;

public class EditDialog : Form
{
    private Button btnOK;
    private Button btnCancel;

    private TextBox tbxNewName;

    private string oldName;

    public EditDialog(string oldName)
    {
        this.FormBorderStyle = FormBorderStyle.Fixed3D;
        this.MaximizeBox = false;
        this.Size = new Size(450, 110);
        this.Text = "Edit \"" + oldName + "\"";
        this.oldName = oldName;

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        btnOK = new Button();
        btnOK.Location = new Point(10, 40);
        btnOK.Size = new Size(50, 30);
        btnOK.Text = "OK";
        btnOK.Click += new System.EventHandler(this.btnOK_Click);
        Controls.Add(btnOK);

        btnCancel = new Button();
        btnCancel.Location = new Point(70, 40);
        btnCancel.Size = new Size(50, 30);
        btnCancel.Text = "Cancel";
        btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        Controls.Add(btnCancel);

        tbxNewName = new TextBox();
        tbxNewName.Location = new Point(10, 10);
        tbxNewName.Size = new Size(420, 30);
        tbxNewName.Text = oldName;
        Controls.Add(tbxNewName);
    }

    public string NewName
    {
        get { return this.tbxNewName.Text; }
    }

    public void btnOK_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    public void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}
