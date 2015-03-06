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
    public partial class Roomeditor : Form
    {
        public List<int> objids = new List<int>();
        public List<Image> objsprs = new List<Image>();
        private List<int> objs = new List<int>();
        public string name;
        public int id;
        public List<PictureBox> pboxes = new List<PictureBox>();
        private Image missingimg = Properties.Resources.target;

        Point dragPoint = Point.Empty;
        bool dragging = false;

        public Roomeditor()
        {
            InitializeComponent();
            rm.MouseClick += rm_MouseClick;
            this.FormClosing += Roomeditor_FormClosing;
        }

        void Roomeditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Resources.Room)Resources.resources[id]).isbeingedited = false;
            Main.UpdateTreeView(Main.resourcelistpublic);
        }

        void PBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //Select the object!
                PictureBox pb = (PictureBox)sender;

                pb.Tag = "true";
                pb.Refresh();
            }
        }

        private void PBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                PictureBox pb = (PictureBox)sender;
                pb.Location = new Point(pb.Location.X + e.X - dragPoint.X, pb.Location.Y + e.Y - dragPoint.Y);
            }
        }

        private void PBox_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        void PBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PictureBox pb = (PictureBox)sender;
                dragging = true;
                dragPoint = new Point(e.X, e.Y);
            }
        }

        void rm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (objlist.Items.Count >= 1 && objlist.SelectedItem != null)
                {
                    PictureBox pb = new PictureBox();
                    pb.Location = e.Location;
                    pb.Image = objsprs[objlist.SelectedIndex];
                    pb.Size = new System.Drawing.Size(pb.Image.Width,pb.Image.Height);
                    pb.BackColor = System.Drawing.Color.Transparent;
                    pboxes.Add(pb);
                    MessageBox.Show(objs[objlist.SelectedIndex].ToString());
                    objids.Add(objs[objlist.SelectedIndex]);
                    pb.Tag = "false";
                    pb.MouseClick += PBox_MouseClick;
                    pb.MouseDown += PBox_MouseDown;
                    pb.MouseUp += PBox_MouseUp;
                    pb.MouseMove += PBox_MouseMove;
                    pb.Paint += PBox_Paint;
                    pb.ContextMenuStrip = objrclick;
                    rm.Controls.Add(pb);
                }
                
            }
        }

        void PBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            if (((string)pb.Tag) == "true")
            {
                Pen whitePen = new Pen(Color.FromArgb(144, 212, 242), 1);
                Pen blackPen = new Pen(Color.Black, 2);

                e.Graphics.DrawRectangle(blackPen, new Rectangle(0, 0, pb.Image.Width - 1, pb.Image.Height - 1));
                e.Graphics.DrawRectangle(whitePen, new Rectangle(0, 0, pb.Image.Width - 1, pb.Image.Height - 1));
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Delete all selected objects
            foreach (PictureBox pb in pboxes)
            {
                if ((string)pb.Tag == "true")
                {
                    rm.Controls.Remove(pb);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //Save the room and close.
            Resources.Room rm = (Resources.Room)Resources.resources[id];
            rm.objid.Clear();
            rm.objpos.Clear();

            int i = 0;
            
            foreach (PictureBox pb in pboxes)
            {
                rm.objid.Add(objids[i]);
                rm.objpos.Add(pb.Location);
                MessageBox.Show("Name: " + ((Resources.Object)Resources.resources[objids[i]]).name + " , ID: " + objids[i].ToString() + ", POS: " + pb.Location.ToString());
                i++;
            }
            this.Close();
        }

        private void Roomeditor_Load(object sender, EventArgs e)
        {
            int i = 0;

            //Get all the objects in the project
            foreach (Resources.Types rs in Resources.resources)
            {
                if (rs.GetType() == typeof(Resources.Object))
                {
                    Resources.Object obj = (Resources.Object)rs;
                    if (obj.defaultsprite != null)
                    {
                        objsprs.Add(obj.defaultsprite);
                        objs.Add(i);
                    }
                    else
                    {
                        objsprs.Add(missingimg);
                        objs.Add(i);
                    }
                    objlist.Items.Add(obj.name);
                    objlist.SelectedIndex = 0;
                }
                i++;
            }

            //Adds all the previously-added objects to the room if necessary
            if (((Resources.Room)Resources.resources[id]).objid.Count > 0)
            {
                //Cast the resource id as a room.
                Resources.Room room = (Resources.Room)Resources.resources[id];
                i = 0;

                objids.Clear();

                foreach (int objid in room.objid)
                {
                    PictureBox pb = new PictureBox();
                    pb.Location = room.objpos[i];

                    Console.WriteLine(objid.ToString());

                    if (((Resources.Object)Resources.resources[objid]).defaultsprite != null)
                    {
                        pb.Image = ((Resources.Object)Resources.resources[objid]).defaultsprite;
                    }
                    else
                    {
                        pb.Image = missingimg;
                    }

                    pb.Size = new System.Drawing.Size(pb.Image.Width, pb.Image.Height);
                    pb.BackColor = System.Drawing.Color.Transparent;
                    pboxes.Add(pb);
                    objids.Add(objid);
                    pb.Tag = "false";
                    pb.MouseClick += PBox_MouseClick;
                    pb.MouseDown += PBox_MouseDown;
                    pb.MouseUp += PBox_MouseUp;
                    pb.MouseMove += PBox_MouseMove;
                    pb.Paint += PBox_Paint;
                    pb.ContextMenuStrip = objrclick;
                    rm.Controls.Add(pb);
                    i++;
                }
            }
            this.Text = name + " - Room Editor";
        }
    }
}
