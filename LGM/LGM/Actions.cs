﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LGM
{
    class Actions
    {

        public static List<string> actions = new List<string>();
        public static List<Category> categories = new List<Category>();

        public static string GetAcionName(int actid)
        {
            //Gets the action's name by chopping up and returning the correct portion of the action's text file which defines the action's title.
            return actions[actid].Substring(0, actions[actid].IndexOf(Environment.NewLine)).Substring(15, actions[actid].Substring(0, actions[actid].IndexOf(Environment.NewLine)).IndexOf("~icon~") - 16);
        }

        public static int GetAcionCategory(int actid)
        {
            //Gets the action's category by chopping up and returning the correct portion of the action's text file which defines the action's category.
            return Convert.ToInt32(actions[actid].Substring(0, actions[actid].IndexOf(Environment.NewLine)).Substring(actions[actid].Substring(0, actions[actid].IndexOf(Environment.NewLine)).IndexOf("~cat~")+6, 1));
        }

        public static void UpdateCategories()
        {
            categories.Clear();

            if (Actions.actions.Count > 0)
            {
                for (int k = 0; k < Actions.actions.Count-1; k++)
                {
                    if (Directory.Exists(Application.StartupPath + "\\actions") && File.Exists(Application.StartupPath + "\\actions\\categories.txt"))
                    {
                        string[] cats = File.ReadAllLines(Application.StartupPath + "\\actions\\categories.txt");
                        categories.Add(new Category(k, cats[k]));
                    };
                }
            }
        }

        public static string FormatAction(Action act)
        {
            switch (act.arguments.Count-1)
            {
                case 0:
                    return string.Format(Actions.actions[act.type], act.arguments[0]);
                case 1:
                    return string.Format(Actions.actions[act.type], act.arguments[0],act.arguments[1]);
                case 2:
                    return string.Format(Actions.actions[act.type], act.arguments[0],act.arguments[1],act.arguments[2]);
                case 3:
                    return string.Format(Actions.actions[act.type], act.arguments[0], act.arguments[1], act.arguments[2],act.arguments[3]);
            }
            return null;
        }

        public static void Add(int id,int evid,int evtype,int type,List<Object> arguments)
        {
            //Adds an action to the object's action list.
            Resources.Object obj = (Resources.Object)Resources.resources[id];
            Action act = new Action(evid);
            act.objid = id;
            act.eventid = evid;
            act.type = type;
            act.arguments.AddRange(arguments);
            //MessageBox.Show("Adding action of type " + act.type.ToString() + " from event of type " + obj.events[evid].type.ToString());
            obj.events[evid].actions.Add(act);
        }

        public static void LoadActions()
        {
            //Load all the actions from the actions directory
            if (Directory.Exists(Application.StartupPath + "\\actions"))
            {
                var files = from file in Directory.GetFiles(Application.StartupPath + "\\actions").ToList<string>() orderby file ascending select file;

                foreach (string file in files)
                {
                    //MessageBox.Show(File.ReadAllText(file));
                    if (File.ReadAllText(file).Contains(@"~~/\ACTION/\~~"))
                    {
                        //If the file contains "~~/\ACTION/\~~", it's an action!
                        actions.Add(File.ReadAllText(file));
                    }
                }
            }
            else
            {
                MessageBox.Show("Error!");
            }
        }

        public class Action
        {
            public int type = 0;
            public int eventid = 0;
            public int objid;
            public int category = 0;
            public List<Object> arguments = new List<object>();

            public Action(int eventid)
            {
                this.eventid = eventid;
            }
        }

        public class Event
        {
            public int id = 0;
            public int type = 0;
            public List<Action> actions = new List<Action>();

            public Event(int type)
            {
                this.type = type;
            }
        }

        public class Category
        {
            public int id;
            public string name;

            public Category (int id,string name)
            {
                this.id = id;
                this.name = name;
            }
        }
    }
}
