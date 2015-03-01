using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGM
{
    class Actions
    {
        public static string GetAcionName(int id,int objid,int actid)
        {
            Resources.Object obj = (Resources.Object)Resources.resources[objid];
            switch (id)
            {
                case 0:
                    return "Move " + Resources.resources[obj.actions[actid].apptoid].name + " to (" + ((Move)obj.actions[actid]).x + "," + ((Move)obj.actions[actid]).y + ")";
                case 1:
                    return "Create instance of " + Resources.resources[((Createid)obj.actions[actid]).crtid].name + " at (" + ((Createid)obj.actions[actid]).x + "," + ((Createid)obj.actions[actid]).y + ")";
            }
            return "Undefined";
        }

        public class Types
        {
            public int id = 0;
            public int eventid = 0;
            public int apptoid = 0;
        }

        public class Move : Types
        {
            public int x = 0;
            public int y = 0;

            public Move()
            {
                id = 0;
            }
        }

        public class Createid : Types
        {
            public int crtid = 0;
            public int x = 0;
            public int y = 0;
            
            public Createid()
            {
                id = 1;
            }
        }
    }
}
