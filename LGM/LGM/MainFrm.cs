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
            //rswindows.DrawMode = TabDrawMode.OwnerDrawFixed;
            rswindows.DrawItem += rswindows_DrawItem;
            rswindows.MouseDown += rswindows_MouseDown;
        }

        void rswindows_DrawItem(object sender, DrawItemEventArgs e)
        {
            //This code will render a "x" mark at the end of the Tab caption. 
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 15, e.Bounds.Top + 4);
            e.Graphics.DrawString(this.rswindows.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        void rswindows_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < rswindows.TabPages.Count; i++)
            {
                Rectangle r = rswindows.GetTabRect(i);
                //Getting the position of the "x" mark.
                Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                if (closeButton.Contains(e.Location))
                {
                    rswindows.TabPages.RemoveAt(i);
                    if (rswindows.Visible && rswindows.TabCount < 1)
                    {
                        rswindows.Visible = false;
                    }
                }
            }
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

                //rslistpnl.Width = DPI.CorrectDPIvalues(198,dx);
                //resourcelist.Width = DPI.CorrectDPIvalues(198, dx); //200, 572

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

            float dx = this.CreateGraphics().DpiX;
            TabPage tbpg = new TabPage("Object " + objcnt.ToString());

            Label events = new Label();
            events.Text = "Events";
            events.Location = new Point(0,DPI.CorrectDPIvalues(5,dx));
            tbpg.Controls.Add(events);
            tbpg.BackColor = Color.White;

            ListBox eventlb = new ListBox();
            eventlb.Location = new Point(0,30);
            eventlb.Size = new Size(DPI.CorrectDPIvalues(20,dx),DPI.CorrectDPIvalues(70,dx));
            tbpg.Controls.Add(eventlb);

            rswindows.TabPages.Add(tbpg);
            objcnt++;
        }
    }
}
