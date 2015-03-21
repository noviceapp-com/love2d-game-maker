using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGM
{
    class DPI
    {
        //DPI-related functions
        public static int CorrectDPIvalues(int val, float per)
        {
            //Get the x/y values to use by multiplying the current ones by the DPI percentage.
            return val * Convert.ToInt32(per) / 100;
        }
    }
}
