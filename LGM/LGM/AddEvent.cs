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
    public partial class AddEvent : Form
    {
        public int eventid = -1;

        public AddEvent()
        {
            InitializeComponent();
        }

        private void AddEvent_Load(object sender, EventArgs e)
        {
            //
        }

        private void button1_Click(object sender, EventArgs e)
        {
            eventid = -1;
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            eventid = 0;
            this.Close();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            eventid = 1;
            this.Close();
        }
    }
}
