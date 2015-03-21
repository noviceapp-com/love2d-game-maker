using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGM
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
            maintoolbar.Renderer = new MyToolStripSystemRenderer();
        }

        private void btnrslist_Click(object sender, EventArgs e)
        {
            rslistpnl.Visible = (btnrslist.Text == ">");
            if (btnrslist.Text == "<") { btnrslist.Text = ">"; } else { btnrslist.Text = "<"; }
        }

        public class MyToolStripSystemRenderer : ToolStripSystemRenderer
        {
            /*
             * A custom toolstriprenderer to make the project look a little nicer.
             * The design currently used by the project is a temporary design heavily based off of "Game Maker" by Mark Overmans.
             * The final project will have a different design, and as such, will likely not need this function.
            */

            public MyToolStripSystemRenderer() { }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                //Basically, putting nothing here keeps the toolstrip from drawing a border, which is exactly what we want. :)
            }
        }
    }
}
