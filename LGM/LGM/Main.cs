using System;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using FastColoredTextBoxNS;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Runtime.InteropServices;
using System.Web;
using System.IO.Compression;

//The mega main class, simplified with region blocks! :D
namespace LGM
{
    public partial class Main : Form
    {
        #region Variable Declaration
        private int childFormNumber = 0;
        public static string projectname = null;
        public static bool issaved = true;

        public static System.Windows.Forms.TreeView resourcelistpublic;
        TreeNode Sprites;
        TreeNode Objects;
        TreeNode Backgrounds;
        TreeNode Sounds;
        TreeNode Rooms;
        TreeNode Scripts;

        public static Image warning = Properties.Resources.warning1;
        public static Image error = Properties.Resources.error1;

        public static string generatedcode = "";
        #endregion

        #region Initialization stuff
        public Main()
        {
            //Initialize the main component
            InitializeComponent();

            //Add Events
            this.FormClosing += Main_Closing;
            toolStrip.Renderer = new MyToolStripSystemRenderer();
            resourcelist.AfterLabelEdit += resourcelist_AfterLabelEdit;
            resourcelist.NodeMouseDoubleClick += resourcelist_NodeMouseDoubleClick;

            //Initialize MDIClientSupport and update the form's title
            MDIClientSupport.SetBevel(this,false);
            UpdateTitle();

            //Define the TreeNode variables
            Sprites = this.resourcelist.Nodes[0];
            Objects = this.resourcelist.Nodes[1];
            Backgrounds = this.resourcelist.Nodes[2];
            Sounds = this.resourcelist.Nodes[3];
            Rooms = this.resourcelist.Nodes[4];
            Scripts = this.resourcelist.Nodes[5];
            resourcelist.LabelEdit = true;

            //Set the TreeView's Images
            for (int i = 0; i < this.resourcelist.Nodes.Count; i++) { this.resourcelist.Nodes[i].ImageIndex = i; this.resourcelist.Nodes[i].SelectedImageIndex = i; }
            UpdateTreeView(resourcelist);

            //Define all the Resource variables
            Resources.DefineResourceArrays();
        }
        
        private void Main_Load(object sender, EventArgs e)
        {
            MdiClient ctlMDI;
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;

                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = Color.FromArgb(144, 212, 242);
                }
                catch
                {
                    // Catch and ignore the error if casting failed.
                }
            }

            Actions.LoadActions();

            CorrectDPI();
            settings.LoadSettings();  
        }
#endregion

        #region DPI-related functions
        public static int DPIIconSize(float dx)
        {
            if (dx == 96)
            {
                return 32;
            }
            else if (dx == 120)
            {
                return 40;
            }
            else if (dx == 144)
            {
                return 48;
            }
            else if (dx == 192)
            {
                return 64;
            }
            return 32;
        }

        public static int CorrectDPIvalues(int val,float per)
        {
            //Get the x/y values to use by multiplying the current ones by the DPI percentage.
            return val * Convert.ToInt32(per) / 100;
        }

        private void CorrectDPI()
        {
            //Corrects the form to be the right size according to the current DPI.
            float dx;
            Graphics g = this.CreateGraphics();

            try
            {
                dx = g.DpiX;

                sizeableTreeView1.Width = CorrectDPIvalues(198, dx);

                foreach (ToolStripItem ti in toolStrip.Items)
                {
                    if (ti.GetType() == typeof(ToolStripButton))
                    {
                        ToolStripButton tsb = (ToolStripButton)ti;
                        tsb.Tag = false;

                        tsb.Width = CorrectDPIvalues(24,dx);
                        tsb.Height = CorrectDPIvalues(24, dx);


                        tsb.MouseDown += tsb_MouseDown;
                        tsb.Click += tsb_Click;
                        tsb.MouseLeave += tsb_Click;
                        tsb.Paint += toolstripbtn_Paint;
                    }
                }

            }
            finally
            {
                g.Dispose();
            }
        }

        private void toolstripbtn_Paint(object sender, PaintEventArgs e)
        {
            //Paints the button correctly.
            ToolStripButton btn = (ToolStripButton)sender;
            Graphics g = this.CreateGraphics();

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.DrawImage((Image)btn.Image, btn.Width / 2 - (CorrectDPIvalues(17, g.DpiX) / 2), btn.Height / 2 - (CorrectDPIvalues(17, g.DpiX) / 2), CorrectDPIvalues(17, g.DpiX), CorrectDPIvalues(17, g.DpiX));
        }
        #endregion

        #region Functions for adding resources

        //These functions all add a resource type to the resource array, and then return their id.
        private int AddSprite()
        {
            //Adds a sprite to the resource list
            if (resourcelist.GetNodeCount(false) > 0 && resourcelist.Nodes[0] != null)
            {
                Resources.resources.Add(new Resources.Sprite());
                Resources.resources[Resources.resourcecnt].name = "Sprite" + Resources.resourcetypecnt[0].ToString();
                TreeNode newsprite = resourcelist.Nodes[0].Nodes.Add(Resources.resources[Resources.resourcecnt].name);
                newsprite.Tag = Resources.resourcecnt;
                newsprite.ToolTipText = Resources.resourcecnt.ToString();
                resourcelist.ShowNodeToolTips = true;
                resourcelist.ExpandAll();
                
                Resources.resourcecnt++; //Increase the current resource count by one, as we've (obviously) just added a resource.
                Resources.resourcetypecnt[0]++; //Increase the number of sprites by one.

                //Signify we made a change and need to save.
                issaved = false;
                UpdateTitle();
                return Resources.resourcecnt-1;
            }
            else
            {
                Error(1);
                return 0; //Just because C# DEMANDS that every code path return a value, even though the program will never execute this line of code. :P
            }
        }

        private int AddObject()
        {
            //Adds an object to the resource list
            if (resourcelist.GetNodeCount(false) > 1 && resourcelist.Nodes[1] != null)
            {
                Resources.resources.Add(new Resources.Object());
                Resources.resources[Resources.resourcecnt].name = "Object" + Resources.resourcetypecnt[1].ToString();
                TreeNode newobj = resourcelist.Nodes[1].Nodes.Add(Resources.resources[Resources.resourcecnt].name);
                newobj.Tag = Resources.resourcecnt;
                newobj.ToolTipText = Resources.resourcecnt.ToString();
                resourcelist.ShowNodeToolTips = true;
                resourcelist.ExpandAll();

                Resources.resourcecnt++; //Increase the current resource count by one, as we've (obviously) just added a resource.
                Resources.resourcetypecnt[1]++; //Increase the number of objects by one.

                //Signify we made a change and need to save.
                issaved = false;
                UpdateTitle();
                return Resources.resourcecnt - 1;
            }
            else
            {
                Error(1);
                return 0; //Just because C# DEMANDS that every code path return a value, even though the program will never execute this line of code. :P
            }
        }

        private int AddBackground()
        {
            //Adds a background to the resource list
            if (resourcelist.GetNodeCount(false) > 2 && resourcelist.Nodes[2] != null)
            {
                Resources.resources.Add(new Resources.Background());
                Resources.resources[Resources.resourcecnt].name = "Background" + Resources.resourcetypecnt[2].ToString();
                TreeNode newbg = resourcelist.Nodes[2].Nodes.Add(Resources.resources[Resources.resourcecnt].name);
                newbg.Tag = Resources.resourcecnt;
                newbg.ToolTipText = Resources.resourcecnt.ToString();
                resourcelist.ShowNodeToolTips = true;
                resourcelist.ExpandAll();

                Resources.resourcecnt++; //Increase the current resource count by one, as we've (obviously) just added a resource.
                Resources.resourcetypecnt[2]++; //Increase the number of backgrounds by one.

                //Signify we made a change and need to save.
                issaved = false;
                UpdateTitle();
                return Resources.resourcecnt - 1;
            }
            else
            {
                Error(1);
                return 0; //Just because C# DEMANDS that every code path return a value, even though the program will never execute this line of code. :P
            }
        }

        private int AddSound()
        {
            //Adds a sound to the resource list
            if (resourcelist.GetNodeCount(false) > 3 && resourcelist.Nodes[3] != null)
            {
                Resources.resources.Add(new Resources.Sound());
                Resources.resources[Resources.resourcecnt].name = "Sound" + Resources.resourcetypecnt[3].ToString();
                TreeNode newsnd = resourcelist.Nodes[3].Nodes.Add(Resources.resources[Resources.resourcecnt].name);
                newsnd.Tag = Resources.resourcecnt;
                newsnd.ToolTipText = Resources.resourcecnt.ToString();
                resourcelist.ShowNodeToolTips = true;
                resourcelist.ExpandAll();

                Resources.resourcecnt++; //Increase the current resource count by one, as we've (obviously) just added a resource.
                Resources.resourcetypecnt[3]++; //Increase the number of sounds by one.

                //Signify we made a change and need to save.
                issaved = false;
                UpdateTitle();
                return Resources.resourcecnt - 1;
            }
            else
            {
                Error(1);
                return 0; //Just because C# DEMANDS that every code path return a value, even though the program will never execute this line of code. :P
            }
        }

        private int AddRoom()
        {
            //Adds a room to the resource list
            if (resourcelist.GetNodeCount(false) > 4 && resourcelist.Nodes[4] != null)
            {
                Resources.resources.Add(new Resources.Room());
                Resources.resources[Resources.resourcecnt].name = "Room" + Resources.resourcetypecnt[4].ToString();
                TreeNode newrm = resourcelist.Nodes[4].Nodes.Add(Resources.resources[Resources.resourcecnt].name);
                newrm.Tag = Resources.resourcecnt;
                newrm.ToolTipText = Resources.resourcecnt.ToString();
                resourcelist.ShowNodeToolTips = true;
                resourcelist.ExpandAll();

                Resources.resourcecnt++; //Increase the current resource count by one, as we've (obviously) just added a resource.
                Resources.resourcetypecnt[4]++; //Increase the number of rooms by one.

                //Signify we made a change and need to save.
                issaved = false;
                UpdateTitle();
                return Resources.resourcecnt - 1;
            }
            else
            {
                Error(1);
                return 0; //Just because C# DEMANDS that every code path return a value, even though the program will never execute this line of code. :P
            }
        }

        private int AddScript()
        {
            //Adds a script to the resource list
            if (resourcelist.GetNodeCount(false) > 5 && resourcelist.Nodes[5] != null)
            {
                Resources.resources.Add(new Resources.Script());
                Resources.resources[Resources.resourcecnt].name = "Script" + Resources.resourcetypecnt[5].ToString();
                TreeNode newscrpt = resourcelist.Nodes[5].Nodes.Add(Resources.resources[Resources.resourcecnt].name);
                newscrpt.Tag = Resources.resourcecnt;
                newscrpt.ToolTipText = Resources.resourcecnt.ToString();
                resourcelist.ShowNodeToolTips = true;
                resourcelist.ExpandAll();

                Resources.resourcecnt++; //Increase the current resource count by one, as we've (obviously) just added a resource.
                Resources.resourcetypecnt[5]++; //Increase the number of scripts by one.

                //Signify we made a change and need to save.
                issaved = false;
                UpdateTitle();
                return Resources.resourcecnt - 1;
            }
            else
            {
                Error(1);
                return 0; //Just because C# DEMANDS that every code path return a value, even though the program will never execute this line of code. :P
            }
        }
        #endregion

        #region Error Code-related functions
        public static void Error(int errorcode)
        {
            //Displays an error message including an error code, and closes the program.
            System.Media.SystemSounds.Hand.Play();
            CustomMessageBox.Show("Love Game Maker has encountered an error and needs to close!" + Environment.NewLine + "ERROR CODE: " + errorcode.ToString(), "Love Game Maker", CustomMessageBox.eDialogButtons.ErrorBtn, error);
            issaved = true;
            Application.Exit();
        }

        public static void Error(int errorcode, string errorstring)
        {
            //Displays an error message including an error code, and closes the program.
            System.Media.SystemSounds.Hand.Play();
            CustomMessageBox.Show(errorstring + Environment.NewLine + "ERROR CODE: " + errorcode.ToString(), "Love Game Maker", CustomMessageBox.eDialogButtons.ErrorBtn, error);
            issaved = true;
            Application.Exit();
        }
        #endregion

        #region Functions currently used for debugging
        private void TestGame()
        {
            //Ensure there's at least one room in the current project before compiling
            bool cont = false;
            
            foreach (Resources.Room rm in Resources.resources){ cont = true; break; }
            if (!cont) { System.Media.SystemSounds.Asterisk.Play(); CustomMessageBox.Show("You need at least one room in your project to compile!", "Love Game Maker", CustomMessageBox.eDialogButtons.OK, warning); return; }

            //Test the game using LOVE 2D
            if (Directory.Exists(Application.StartupPath+"\\temp"))
            {
                try
                {
                    Directory.Delete(Application.StartupPath+"\\temp",true);
                }
                catch
                {
                    //Error(1);
                }
            }
            Directory.CreateDirectory(Application.StartupPath + "\\temp");
            GeneratedCode.GenerateCode();

            if (Directory.Exists(settings.love2dpath))
            {
                Process sevenzip = new Process();
                sevenzip.StartInfo = new ProcessStartInfo(Application.StartupPath + "\\7z.exe", "a game.zip -r " + '"' + Application.StartupPath + "\\temp\\*" + '"');
                sevenzip.Start();
                sevenzip.WaitForExit();
                File.Move(Application.StartupPath+"\\game.zip",Application.StartupPath+"\\temp\\game.love");
                Process love2d = new Process();
                love2d.StartInfo = new ProcessStartInfo(settings.love2dpath+"\\love.exe",'"'+Application.StartupPath+"\\temp\\game.love"+'"');
                love2d.Start();
                love2d.WaitForExit();

                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\LOVE\\LGMtestgame") && File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\LOVE\\LGMtestgame\\error.txt"))
                {
                    System.Media.SystemSounds.Hand.Play();
                    CustomMessageBox.Show("It appears something has gone wrong with your code! Please ensure" + Environment.NewLine + "your code is correct and try again.", "Love Game Maker", CustomMessageBox.eDialogButtons.OK, error);
                }
            }
        }
        
        private void ShowNewForm(object sender, EventArgs e)
        {
            //This function is one generated by Visual Studio when you add an "MDI Parent" form.
            //Right now it's just for testing stuff. We'll remove it when it's un-needed. ;)
            Form childForm = new PowerfulSample();
            childForm.MdiParent = this;
            childForm.Text = "Object" + childFormNumber++;
            childForm.Show();
            issaved = false;
            UpdateTitle();
        }
        #endregion

        #region Project Naming functions
        private string getprojectname()
        {
            //This function gets the name of the project.
            if (projectname == null)
            {
                return "Untitled";
            }
            else
            {
                return ""; //TODO: return the actual project name.
            }
        }

        private void UpdateTitle()
        {
            //This function updates the Window's title based upon the project's name.
            if (issaved)
            {
                this.Text = getprojectname() + " - Love Game Maker";
            }
            else
            {
                this.Text = getprojectname() + "* - Love Game Maker";
            }
        }
        #endregion

        #region Resource List handling
        public static void UpdateTreeView(System.Windows.Forms.TreeView resourcelist)
        {
            //Define variables
            ImageList resourceimagelist = new ImageList();
            resourceimagelist.Images.Add(Properties.Resources.sprite1);
            resourceimagelist.Images.Add(Properties.Resources.object1);
            resourceimagelist.Images.Add(Properties.Resources.bg1);
            resourceimagelist.Images.Add(Properties.Resources.sound1);
            resourceimagelist.Images.Add(Properties.Resources.room1);
            resourceimagelist.Images.Add(Properties.Resources.script1);
            resourcelist.ImageList = resourceimagelist;


            //Clear the TreeView
            for (int k = 0; k < resourcelist.Nodes.Count; k++)
            {
                resourcelist.Nodes[k].Nodes.Clear();
            }

            //Add the resources to the treeview's list of nodes
            for (int i = 0; i < Resources.resources.Count; i++)
            {
                if (Resources.resources[i] is Resources.Sprite)
                {
                    TreeNode tn = new TreeNode(Resources.resources[i].name);
                    tn.Tag = i;
                    tn.ToolTipText = i.ToString();
                    tn.ImageIndex = 0; tn.SelectedImageIndex = 0;
                    resourcelist.Nodes[0].Nodes.Add(tn);
                }
                else if (Resources.resources[i] is Resources.Object)
                {
                    TreeNode tn = new TreeNode(Resources.resources[i].name);
                    tn.Tag = i;
                    tn.ToolTipText = i.ToString();
                    tn.ImageIndex = 1; tn.SelectedImageIndex = 1;
                    resourcelist.Nodes[1].Nodes.Add(tn);
                }
                else if (Resources.resources[i] is Resources.Background)
                {
                    TreeNode tn = new TreeNode(Resources.resources[i].name);
                    tn.Tag = i;
                    tn.ToolTipText = i.ToString();
                    tn.ImageIndex = 2; tn.SelectedImageIndex = 2;
                    resourcelist.Nodes[2].Nodes.Add(tn);
                }
                else if (Resources.resources[i] is Resources.Sound)
                {
                    TreeNode tn = new TreeNode(Resources.resources[i].name);
                    tn.Tag = i;
                    tn.ToolTipText = i.ToString();
                    tn.ImageIndex = 3; tn.SelectedImageIndex = 3;
                    resourcelist.Nodes[3].Nodes.Add(tn);
                }
                else if (Resources.resources[i] is Resources.Room)
                {
                    TreeNode tn = new TreeNode(Resources.resources[i].name);
                    tn.Tag = i;
                    tn.ToolTipText = i.ToString();
                    tn.ImageIndex = 4; tn.SelectedImageIndex = 4;
                    resourcelist.Nodes[4].Nodes.Add(tn);
                }
                else if (Resources.resources[i] is Resources.Script)
                {
                    TreeNode tn = new TreeNode(Resources.resources[i].name);
                    tn.Tag = i;
                    tn.ToolTipText = i.ToString();
                    tn.ImageIndex = 5; tn.SelectedImageIndex = 5;
                    resourcelist.Nodes[5].Nodes.Add(tn);
                }
            }
        }

        void resourcelist_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            Resources.resources[Convert.ToInt32(e.Node.Tag)].name = e.Label;
        }

        private void RenameResource()
        {
            //The super-simple way to re-name resources!
            if (resourcelist.SelectedNode.Parent != null)
            {
                resourcelist.SelectedNode.BeginEdit();
            }
        }

        private void DeleteResource()
        {
            //Delete the selected resource.

            if (resourcelist.SelectedNode.Parent != null && !Resources.resources[Convert.ToInt32(resourcelist.SelectedNode.Tag)].isbeingedited)
            {
                TreeNode parnode = resourcelist.SelectedNode.Parent;
                TreeNode selnode = resourcelist.SelectedNode;
                Resources.resources.Remove(Resources.resources[Convert.ToInt32(resourcelist.SelectedNode.Tag)]);
                Resources.resourcecnt--;
                UpdateTreeView(resourcelist);
                resourcelistpublic = resourcelist;

                if (resourcelist.Nodes[parnode.Index].Nodes.Count > selnode.Index)
                {
                    resourcelist.SelectedNode = resourcelist.Nodes[parnode.Index].Nodes[selnode.Index];
                }
            }
            
        }
        #endregion

        #region All the buttons/Menu items
        private void tsb_MouseDown(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).Tag = true;
        }
        
        private void tsb_Click(object sender, EventArgs e)
        {
            ((ToolStripButton)sender).Tag = false;
        }


        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(true);
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void verticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void horizontallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(false);
        }

        private void resourceListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sizeableTreeView1.Visible = resourceListToolStripMenuItem.Checked;
            if (sizeableTreeView1.Visible)
            {
                sizeableTreeView1.Width = CorrectDPIvalues(198, this.CreateGraphics().DpiX);
            }
        }

        private void testbtn_Click(object sender, EventArgs e)
        {
            //Open the game in Love 2D for testing.
            TestGame();
        }

        private void testGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestGame();
        }
        
        private void spritebtn_Click(object sender, EventArgs e)
        {
            //Adds a sprite to the resource list and opens it in the sprite editor
            Spriteeditor spreditr = new Spriteeditor();
            spreditr.MdiParent = this;
            spreditr.id = AddSprite();
            Resources.resources[spreditr.id].isbeingedited = true;
            spreditr.name = Resources.resources[spreditr.id].name;
            Resources.Sprite spr = (Resources.Sprite)Resources.resources[spreditr.id];

            if (spr.sprites.Count > 0)
            {
                int i = 0;
                
                foreach (Image frm in spr.sprites)
                {
                    if (spr.sprites[i] != null)
                    {
                        spreditr.sprites.Add(spr.sprites[i]);
                    }
                    i++;
                }
            }

            UpdateTreeView(resourcelist);
            resourcelistpublic = resourcelist;
            
            spreditr.Show();
        }
        
        private void objectbtn_Click(object sender, EventArgs e)
        {
            Objecteditor objedtr = new Objecteditor();
            objedtr.id = AddObject();
            objedtr.MdiParent = this;
            
            Resources.Object obj = (Resources.Object)Resources.resources[objedtr.id];
            objedtr.name = obj.name;
            objedtr.objspr = obj.defaultsprite;
            Resources.resources[objedtr.id].isbeingedited = true;
            
            UpdateTreeView(resourcelist);
            resourcelistpublic = resourcelist;
            
            objedtr.Show();
        }
        
        private void editGeneratedCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void settingsbtn_Click(object sender, EventArgs e)
        {
            options options = new options();
            options.ShowDialog();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            options options = new options();
            options.ShowDialog();
        }
        
        private void bgbtn_Click(object sender, EventArgs e)
        {
            AddBackground();
            UpdateTreeView(resourcelist);
        }

        private void soundbtn_Click(object sender, EventArgs e)
        {
            AddSound();
            UpdateTreeView(resourcelist);
        }

        private void roombtn_Click(object sender, EventArgs e)
        {
            Roomeditor rmeditr = new Roomeditor();
            rmeditr.MdiParent = this;
            rmeditr.id = AddRoom();
            rmeditr.name = Resources.resources[rmeditr.id].name;
            rmeditr.Text = rmeditr.name;
            Resources.resources[rmeditr.id].isbeingedited = true;

            UpdateTreeView(resourcelist);
            resourcelistpublic = resourcelist;

            rmeditr.Show();
        }

        private void scriptbtn_Click(object sender, EventArgs e)
        {
            AddScript();
            UpdateTreeView(resourcelist);
        }
        private void renameResourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Renames the selected resource
            RenameResource();
        }
        private void deleteResourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteResource();
        }
        void resourcelist_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Open a resource if it's double-clicked on the resource list.
            if (e.Node.Parent != null && !Resources.resources[Convert.ToInt32(e.Node.Tag)].isbeingedited)
            {
                if (Resources.resources[Convert.ToInt32(e.Node.Tag)].GetType() == typeof(Resources.Sprite))
                {
                    Resources.resources[Convert.ToInt32(e.Node.Tag)].isbeingedited = true;
                    Spriteeditor spreditr = new Spriteeditor();
                    spreditr.MdiParent = this;
                    spreditr.name = Resources.resources[Convert.ToInt32(e.Node.Tag)].name;
                    spreditr.id = Convert.ToInt32(e.Node.Tag);
                    Resources.Sprite spr = (Resources.Sprite)Resources.resources[Convert.ToInt32(e.Node.Tag)];

                    int i = 0;

                    foreach (Image frm in spr.sprites)
                    {
                        if (frm != null)
                        {
                            spreditr.sprites.Add(frm);
                        }
                        i++;
                    }

                    UpdateTreeView(resourcelist);
                    resourcelistpublic = resourcelist;

                    spreditr.Show();
                }
                else if (Resources.resources[Convert.ToInt32(e.Node.Tag)].GetType() == typeof(Resources.Object))
                {
                    Resources.resources[Convert.ToInt32(e.Node.Tag)].isbeingedited = true;
                    Objecteditor objeditr = new Objecteditor();
                    objeditr.MdiParent = this;
                    objeditr.name = Resources.resources[Convert.ToInt32(e.Node.Tag)].name;
                    objeditr.id = Convert.ToInt32(e.Node.Tag);
                    Resources.Object obj = (Resources.Object)Resources.resources[Convert.ToInt32(e.Node.Tag)];
                    objeditr.objspr = obj.defaultsprite;

                    UpdateTreeView(resourcelist);
                    resourcelistpublic = resourcelist;

                    objeditr.Show();

                }
                else if (Resources.resources[Convert.ToInt32(e.Node.Tag)].GetType() == typeof(Resources.Room))
                {
                    Resources.resources[Convert.ToInt32(e.Node.Tag)].isbeingedited = true;
                    Roomeditor rmeditr = new Roomeditor();
                    rmeditr.MdiParent = this;
                    rmeditr.name = Resources.resources[Convert.ToInt32(e.Node.Tag)].name;
                    rmeditr.id = Convert.ToInt32(e.Node.Tag);
                    Resources.Room rm = (Resources.Room)Resources.resources[Convert.ToInt32(e.Node.Tag)];

                    UpdateTreeView(resourcelist);
                    resourcelistpublic = resourcelist;

                    rmeditr.Show();
                    
                }

            }
        }
        #endregion

        private void Save(bool saveas)
        {
            //This function saves the project
            if (!saveas)
            {
                //TODO: Save the project
            }
            else
            {
                //TODO: Save the project as something.
            }
            issaved = true;
            UpdateTitle();
        }

        private void Main_Closing(object sender, FormClosingEventArgs e)
        {
            //Stops the user from exiting the application if there are unsaved changes.
            if (!issaved)
            {
                System.Media.SystemSounds.Asterisk.Play();
                DialogResult areusure = CustomMessageBox.Show("You have unsaved changes! Would you like to save your work first?", "Love Game Maker", CustomMessageBox.eDialogButtons.YesNoCancel, warning);
                if (areusure == System.Windows.Forms.DialogResult.Cancel)
                {
                    //Don't close the form!
                    e.Cancel = true;
                }
                else if (areusure == System.Windows.Forms.DialogResult.Yes)
                {
                    //Save the file before closing
                    Save(false);
                }
            }
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

        }
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
