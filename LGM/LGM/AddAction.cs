using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGM
{
    public partial class AddAction : Form
    {

        public List<Object> arguments = new List<object>();
        private List<TextBox> tboxes = new List<TextBox>();
        public int objid = 0;
        public int eventid = 0;
        public int actid;
        public int type = 0;
        public int u = 0;

        public AddAction()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void AddAction_Load(object sender, EventArgs e)
        {
            if (actid != -1)
            {
                u = ((Resources.Object)Resources.resources[objid]).events[eventid].actions[actid].type;
            }
            else
            {
                u = type;
            }
            MessageBox.Show("ACTION: " + u.ToString());
            foreach (string str in GetSubstringsInString(Actions.actions[u], "~arg~",6))
            {
                //Gets all the positions of "~arg~"
                TextBox tbox = new TextBox();
                tbox.Location = new Point(139,(tboxes.Count*32)+16);
                tbox.Size = new Size(196,26);
                tbox.Tag = Convert.ToInt32(str);
                argumentboxes.Controls.Add(tbox);
                
                if (str == "1")
                {
                    Button btn = new Button();
                    btn.Location = new Point(351, (tboxes.Count * 32) + 16);
                    btn.Size = new Size(26, 26);
                    argumentboxes.Controls.Add(btn);
                }
                tboxes.Add(tbox);
            }
            appliestogbox.Enabled = (GetSubstringsInString(Actions.actions[u], "~appliesto~", 12)[0] == "1");
            Actions.actions[u].Substring(0, Actions.actions[u].IndexOf(Environment.NewLine)).Substring(15, Actions.actions[u].Substring(0, Actions.actions[u].IndexOf(Environment.NewLine)).IndexOf("~arg~") - 16);
        }

        public List<string> GetSubstringsInString(string source, string searchString,int offset)
        {
            List<string> ret = new List<string>();
            int len = searchString.Length;
            int start = -len;

            while (true)
            {
                start = source.IndexOf(searchString, start + len);
                if (start == -1)
                {
                    break;
                }
                else
                {
                    ret.Add(source.Substring(start + offset, 1));
                }
            }
            return ret;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            arguments.Clear();
            bool shouldclose = true;

            foreach (TextBox tbox in tboxes)
            {
                if (tbox.Text == "")
                {
                    System.Media.SystemSounds.Hand.Play();
                    CustomMessageBox.Show("You must fill out all the arguments!","Love Game Maker",CustomMessageBox.eDialogButtons.OK,Main.error);
                    shouldclose = false;
                    break;
                }
                arguments.Add(tbox.Text);
            }

            if (shouldclose) { this.DialogResult = System.Windows.Forms.DialogResult.OK; }
        }
    }
}
