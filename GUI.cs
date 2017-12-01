using System;
using System.Drawing;
using System.Windows.Forms;

public class GUI : Form
{
    private IBusinessLayer businesslayer;

    public GUI(IBusinessLayer businesslayer)
	{
        this.businesslayer = businesslayer;
    }
}
