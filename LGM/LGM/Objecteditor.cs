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
        private bool OKpressed = false;

        public Objecteditor()
        {
            InitializeComponent();
            this.FormClosing += Objecteditor_FormClosing;
        }

        void Objecteditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Resources.resources[id].isbeingedited = false;
            Main.UpdateTreeView(Main.resourcelistpublic);

            if (!OKpressed)
            {
                Resources.Object obj = (Resources.Object)Resources.resources[id];
                obj.actions.Clear();
                foreach (Actions.Types bkpaction in obj.bkpactions)
                {
                    obj.actions.Add(bkpaction);
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OKpressed = true;
            objspr = pictureBox1.Image;
            Resources.resources[id].name = textBox1.Text;
            Resources.Object obj = (Resources.Object)Resources.resources[id];
            obj.events.Clear();

            foreach (int i in events)
            {
                obj.events.Add(i);
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

            foreach (int eventid in ((Resources.Object)Resources.resources[id]).events)
            {
                events.Add(eventid);
                eventlist.Items.Add(GetEventName(eventid));
            }

            foreach (Actions.Types actionids in obj.actions)
            {
                obj.bkpactions.Add(actionids);
            }

            UpdateActionList();

            if (events.Count > 0)
            {
                eventlist.SelectedIndex = 0;
            }

            this.Text = name + " - Object Editor";
            textBox1.Text = name;
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
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
            int i = 0;

            foreach (Actions.Types eventid in obj.actions)
            {
                if (eventid.eventid == eventlist.SelectedIndex)
                {
                    actionlist.Items.Add(Actions.GetAcionName(eventid.id,id,i));
                }
                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Resources.Object obj = (Resources.Object)Resources.resources[id];
            Actions.Types act = new Actions.Move();
            MessageBox.Show(eventlist.SelectedIndex.ToString());
            MessageBox.Show(events[eventlist.SelectedIndex].ToString());
            act.eventid = eventlist.SelectedIndex;
            act.eventtype = events[eventlist.SelectedIndex];
            ((Actions.Move)act).x = 5;
            ((Actions.Move)act).y = 7;
            obj.actions.Add(act);
            UpdateActionList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Resources.Object obj = (Resources.Object)Resources.resources[id];
            Actions.Types act = new Actions.Createid();
            act.eventid = eventlist.SelectedIndex;
            act.eventtype = events[eventlist.SelectedIndex];
            obj.actions.Add(act);
            UpdateActionList();
        }

        private void btnaddevent_Click(object sender, EventArgs e)
        {
            AddEvent ae = new AddEvent();
            ae.ShowDialog();

            if (ae.eventid != -1 && !events.Contains(ae.eventid))
            {
                events.Add(ae.eventid);
                eventlist.Items.Add(GetEventName(ae.eventid));
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
