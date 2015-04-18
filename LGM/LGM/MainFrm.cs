using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*WARNING!!!
 * THE FOLLOWING CODE WAS MADE AS QUICKLY AS POSSIBLE SIMPLY AS A WORKING PROTOTYPE.
 * IT'S COMPLETELY UN-OPTIMIZED, IM-PROPERLY STRUCTURED, AND ACTUALLY QUITE TERRIBLE!
 * 
 * YOU'VE BEEN WARNED!!!!!
 */

namespace LGM
{
    public partial class MainFrm : Form
    {
        int objcnt = 0;

        public MainFrm()
        {
            InitializeComponent();
            maintoolbar.Renderer = new MyToolStripSystemRenderer();

            CorrectDPI();
        }

        public class MyToolStripSystemRenderer : ToolStripSystemRenderer
        {
            //A custom toolstriprenderer to make the project look a little nicer.

            public MyToolStripSystemRenderer() { }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                //Basically, putting nothing here keeps the toolstrip from drawing a border, which is exactly what we want. :)
            }
        }

        private void CorrectDPI()
        {
            //Corrects the form to be the right size according to the current DPI.
            float dx;
            Graphics g = this.CreateGraphics();

            try
            {
                dx = g.DpiX;

                resourcelist.Width = DPI.CorrectDPIvalues(198, dx); //200, 572

                foreach (ToolStripItem ti in maintoolbar.Items)
                {
                    if (ti.GetType() == typeof(ToolStripButton))
                    {
                        ToolStripButton tsb = (ToolStripButton)ti;

                        tsb.Width = DPI.CorrectDPIvalues(24, dx);
                        tsb.Height = DPI.CorrectDPIvalues(24, dx);
                        //MessageBox.Show(tsb.Height.ToString());
                    }
                }

            }
            finally
            {
                g.Dispose();
            }
        }

        #region Control-related functions
        private void btnrslist_Click(object sender, EventArgs e)
        {
            /* Change button's arrow from ">" to "<" and vice versa depending on
               if the resource list is shown or not.*/

            rslistpnl.Visible = (btnrslist.Text == ">");
            if (btnrslist.Text == "<") { btnrslist.Text = ">"; viewresourceListmi.Checked = false; } else { btnrslist.Text = "<"; viewresourceListmi.Checked = true; }
        }

        private void newmi_Click(object sender, EventArgs e)
        {
            //New Document
        }

        private void openmi_Click(object sender, EventArgs e)
        {
            //Open Document
        }

        private void savemi_Click(object sender, EventArgs e)
        {
            //Save Document
        }

        private void saveasmi_Click(object sender, EventArgs e)
        {
            //Save Document AS
        }

        private void testmi_Click(object sender, EventArgs e)
        {
            //Test Game
        }

        private void buildmi_Click(object sender, EventArgs e)
        {
            //Build Game
        }

        private void exitmi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newbtn_Click(object sender, EventArgs e)
        {
            //
        }
        
        private void viewtoolbarmi_Click(object sender, EventArgs e)
        {
            viewtoolbarmi.Checked = !viewtoolbarmi.Checked;
            maintoolbar.Visible = viewtoolbarmi.Checked;
        }

        private void viewresourceListmi_Click(object sender, EventArgs e)
        {
            viewresourceListmi.Checked = !viewresourceListmi.Checked;
            rslistpnl.Visible = viewresourceListmi.Checked;

            if (btnrslist.Text == "<") { btnrslist.Text = ">"; } else { btnrslist.Text = "<"; }
        }
        #endregion

        private void objectbtn_Click(object sender, EventArgs e)
        {
            if (!rswindows.Visible)
            {
                rswindows.Visible = true;
            }

            TabPage tbpg = new TabPage("Object " + objcnt.ToString());

            //tbpg.Controls.Add(new Label());

            rswindows.TabPages.Add(tbpg);
            objcnt++;
        }

        
    }
}
