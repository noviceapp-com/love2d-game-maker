using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LGM
{
    class Resources
    {
        public static int resourcecnt = 0; //The amount of resources in the current project.
        public static int[] resourcetypecnt = new int[9]; //0=Number of sprites, 1=Number of Objects, 2=Number of Backgrounds, etc.
        public static List<Types> resources = new List<Types>(); //The types of resources corrisponding to each node on the resourcelist treeview.

        public static void DefineResourceArrays()
        {
            //Define all those arrays declared above
            for (int i = 0; i < 5; i++)
            {
                resourcetypecnt[i] = 0;
            }
        }

        public class Types
        {
            public Types(){}
            public string data = "";
            public string name = "Untitled Resource";
            public bool isbeingedited = false;
        }
        
        public class Sprite:Types
        { 
            public List<Image> sprites = new List<Image>(); //The sprite's images
            public Point origin = new Point(0,0); //The sprite's origin.

            public Sprite(){}
        }

        public class Object:Types
        {
            public Image defaultsprite = null;
            public List<Actions.Event> events = new List<Actions.Event>();
            public List<Actions.Action> bkpactions = new List<Actions.Action>();
        }

        public class Background:Types
        {
            public Background(){}
        }

        public class Sound : Types
        {
            public Sound(){}
        }

        public class Room : Types
        {
            public List<Point> objpos = new List<Point>();
            public List<int> objid = new List<int>();

            public Room(){}
        }
        public class Script : Types
        {
            public Script(){}
        }
    }
}
