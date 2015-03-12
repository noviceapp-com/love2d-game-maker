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
    public partial class Objecteditor : Form
    {
        public string name;
        public int id;
        public Image objspr;
        public int sprid;
        private List<Resources.Sprite> sprites = new List<Resources.Sprite>();
        private Image missingimg = Properties.Resources.target;
        public List<int> events = new List<int>();
        public List<int> actions = new List<int>();
        private List<int> catxpositions = new List<int>();
        private bool OKpressed = false;

        public Objecteditor()
        {
            InitializeComponent();
            this.FormClosing += Objecteditor_FormClosing;

            actionlist.MouseDoubleClick += actionlist_MouseDoubleClick;
        }

        void Objecteditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Resources.resources[id].isbeingedited = false;
            Main.UpdateTreeView(Main.resourcelistpublic);

            if (!OKpressed)
            {
                Resources.Object obj = (Resources.Object)Resources.resources[id];
                obj.events[eventlist.SelectedIndex].actions.Clear();
                foreach (Actions.Action bkpaction in obj.bkpactions)
                {
                    obj.events[eventlist.SelectedIndex].actions.Add(bkpaction);
                }
            }
        }

        void actionlist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.actionlist.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                AddAction addact = new AddAction();
                addact.actid = index;
                addact.objid = id;
                addact.eventid = eventlist.SelectedIndex;
                if (addact.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Actions.Action act = ((Resources.Object)Resources.resources[id]).events[eventlist.SelectedIndex].actions[index];
                    act.arguments = addact.arguments;
                    UpdateActionList();
                }
            }
            else
            {
                //ERROR
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OKpressed = true;
            objspr = pictureBox1.Image;
            Resources.resources[id].name = textBox1.Text;
            Resources.Object obj = (Resources.Object)Resources.resources[id];
            //obj.events.Clear();

            foreach (int i in events)
            {
                //obj.events.Add(i);
            }

            obj.defaultsprite = objspr;
            this.Close();
        }

        private void Objecteditor_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = objspr;
            Resources.Object obj = (Resources.Object)Resources.resources[id];
            obj.bkpactions.Clear();

            int i = 0;

            //Get all the sprites in the project
            foreach (Resources.Types rs in Resources.resources)
            {
                if (rs.GetType() == typeof(Resources.Sprite))
                {
                    Resources.Sprite spr = (Resources.Sprite)rs;
                    if (spr.sprites.Count > 0 && spr.sprites != null)
                    {
                        sprites.Add(spr);
                    }
                    else
                    {
                        sprites.Add(null);
                    }
                    comboBox1.Items.Add(spr.name);
                    comboBox1.SelectedIndex = 0;
                }
                i++;
            }

            foreach (Actions.Event eventid in ((Resources.Object)Resources.resources[id]).events)
            {
                events.Add(eventid.type);
                eventlist.Items.Add(GetEventName(eventid.type));
            }

            if (obj.events.Count > 0)
            {
                foreach (Actions.Action actionids in obj.events[0].actions)
                {
                    obj.bkpactions.Add(actionids);
                }
            }

            if (Actions.actions.Count > 0)
            {
                Actions.UpdateCategories();
                actionstab.Controls.Clear();
                List<TabPage> tbpgs = new List<TabPage>();
                tbpgs.Clear();

                foreach (Actions.Category cat in Actions.categories)
                {
                    TabPage tbpg = new TabPage(cat.name);
                    tbpg.BackColor = Color.White;
                    tbpg.Tag = cat.id;
                    tbpgs.Add(tbpg);
                }
                
                for (int k = 0; k < Actions.actions.Count; k++)
                {
                    //MessageBox.Show("K: " + k.ToString(),"Debug Message");
                    //MessageBox.Show("!!!!" + Actions.GetAcionCategory(0).ToString() + "!!!!", "Debug Message");
                    Button actnbtn = new Button();
                    actnbtn.Location = GetActionbtnpositioning(Actions.GetAcionCategory(k),k);
                    actnbtn.Size = new System.Drawing.Size(Main.CorrectDPIvalues(50, this.CreateGraphics().DpiX), Main.CorrectDPIvalues(39, this.CreateGraphics().DpiX));
                    actnbtn.Tag = k;
                    actnbtn.FlatStyle = FlatStyle.System;
                    (new ToolTip()).SetToolTip(actnbtn, Actions.GetAcionName(k));
                    actnbtn.Click += new System.EventHandler(AddAction);
                    actnbtn.Text = Actions.GetAcionName(k);

                    foreach (TabPage tbpgg in tbpgs)
                    {
                        if ((int)tbpgg.Tag == Actions.GetAcionCategory(k))
                        {
                            tbpgg.Controls.Add(actnbtn);
                        }
                    }
                }

                foreach (TabPage tbpgg in tbpgs)
                {
                    actionstab.Controls.Add(tbpgg);
                }
            }
            

            UpdateActionList();

            if (events.Count > 0)
            {
                eventlist.SelectedIndex = 0;
            }

            this.Text = name + " - Object Editor";
            textBox1.Text = name;
        }

        private Point GetActionbtnpositioning(int catid,int k)
        {
            //NOTE: TEMPORARY FUNCTION. Will fix soon. ;)
            //Main.CorrectDPIvalues(62, this.CreateGraphics().DpiX);
            Point pnt = new Point();

            /*if (catxpositions.Count > 0 && catxpositions[catid] > Main.CorrectDPIvalues(62, this.CreateGraphics().DpiX))
            {
                pnt.Y += 70;
            }
            else
            {
                catxpositions.Add(k);
            }*/
            pnt.X = k * Main.CorrectDPIvalues(62, this.CreateGraphics().DpiX);
            pnt.X += Main.CorrectDPIvalues(5, this.CreateGraphics().DpiX);
            pnt.Y = 0;

            return pnt;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (sprites[comboBox1.SelectedIndex].sprites[0] != null)
                {
                    pictureBox1.Image = sprites[comboBox1.SelectedIndex].sprites[0];
                }
                else
                {
                    pictureBox1.Image = missingimg;
                }
            }
            catch (Exception ex)
            {
                pictureBox1.Image = missingimg;
            }
        }

        private void eventlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateActionList();
        }

        private string GetEventName(int id)
        {
            //Gets the name of the event based upon it's id.
            switch (id)
            {
                case 0:
                    return "Create";
                case 1:
                    return "Step";
            }
            return "Undefined";
        }

        private void UpdateActionList()
        {
            //Updates the list of actions
            actionlist.Items.Clear();
            Resources.Object obj = (Resources.Object)Resources.resources[id];

            if (obj.events.Count > 0 && eventlist.SelectedIndex > -1 && obj.events[eventlist.SelectedIndex].actions.Count > 0)
            {
                for (int i = 0; i < obj.events[eventlist.SelectedIndex].actions.Count; i++)
                {
                    if (obj.events[eventlist.SelectedIndex].actions[i].eventid == eventlist.SelectedIndex)
                    {
                        string args = "";

                        foreach (Object arg in obj.events[eventlist.SelectedIndex].actions[i].arguments)
                        {
                            args += arg.ToString() + ",";
                        }
                        actionlist.Items.Add(Actions.GetAcionName(obj.events[eventlist.SelectedIndex].actions[i].type) + " (" + args.Substring(0,args.Length-1) + ")");
                    }
                }
            }
        }

        private void AddAction(object sender, EventArgs e)
        {
            if (eventlist.Items.Count > 0)
            {
                LGM.AddAction addact = new LGM.AddAction();
                addact.actid = -1;
                addact.type = Convert.ToInt32(((Button)sender).Tag);
                addact.objid = id;
                addact.eventid = eventlist.SelectedIndex;

                if (addact.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Actions.Add(id, eventlist.SelectedIndex, events[eventlist.SelectedIndex], addact.u, addact.arguments);
                    Resources.Object obj = (Resources.Object)Resources.resources[id];
                    UpdateActionList();
                }
            }
            else
            {
                System.Media.SystemSounds.Asterisk.Play();
                CustomMessageBox.Show("You must add an event first!","Love Game Maker",CustomMessageBox.eDialogButtons.OK,Main.warning);
            }
        }

        private void btnaddevent_Click(object sender, EventArgs e)
        {
            AddEvent ae = new AddEvent();
            ae.ShowDialog();

            if (ae.eventid != -1 && !events.Contains(ae.eventid))
            {
                events.Add(ae.eventid);
                ((Resources.Object)Resources.resources[id]).events.Add(new Actions.Event(ae.eventid));
                eventlist.Items.Add(GetEventName(ae.eventid));
                MessageBox.Show(((Resources.Object)Resources.resources[id]).events[0].type.ToString());
                if (eventlist.SelectedIndex < 0)
                {
                    eventlist.SelectedIndex = 0;
                }
                else
                {
                    eventlist.SelectedIndex = eventlist.Items.Count-1;
                }
            }
        }
    }
}
