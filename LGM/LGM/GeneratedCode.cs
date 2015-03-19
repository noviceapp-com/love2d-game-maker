using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace LGM
{
    class GeneratedCode
    {

        public static void GenerateCode()
        {
            //Generate lua code for LOVE 2D
            if (Directory.Exists(Application.StartupPath + "\\codetemplates") && File.Exists(Application.StartupPath + "\\codetemplates\\main.template") && File.Exists(Application.StartupPath + "\\codetemplates\\room.template") && File.Exists(Application.StartupPath + "\\codetemplates\\object.template") && File.Exists(Application.StartupPath + "\\libraries\\middleclass.lua") && Directory.Exists(Application.StartupPath + "\\actions"))
            {
                string objtemplate = File.ReadAllText(Application.StartupPath + "\\codetemplates\\object.template");
                string roomtemplate = File.ReadAllText(Application.StartupPath + "\\codetemplates\\room.template");
                string maintemplate = File.ReadAllText(Application.StartupPath + "\\codetemplates\\main.template");

                List<Resources.Object> objs = new List<Resources.Object>();
                List<Resources.Room> rms = new List<Resources.Room>();

                string roomrequires = "";

                int i = 0;
                foreach (Resources.Types rs in Resources.resources)
                {
                    if (rs.GetType() == typeof(Resources.Object))
                    {
                        Resources.Object obj = (Resources.Object)rs;
                        objs.Add(obj);
                        int a = 0;

                        string createlines = "--The create event";
                        string steplines = "--The step event";

                        foreach (Actions.Event evt in obj.events)
                        {
                            foreach (Actions.Action act in evt.actions)
                            {
                                MessageBox.Show("!!!" + Actions.FormatAction(act).Substring(Actions.FormatAction(act).IndexOf(Environment.NewLine) + 2) + "!!!");
                                if (evt.type == 0)
                                {
                                    createlines+=Environment.NewLine+Actions.FormatAction(act).Substring(Actions.FormatAction(act).IndexOf(Environment.NewLine) + 2);
                                }
                                else if (evt.type == 1)
                                {
                                    steplines+=Environment.NewLine + Actions.FormatAction(act).Substring(Actions.FormatAction(act).IndexOf(Environment.NewLine) + 2);
                                }
                                a++;
                            }
                        }

                        MessageBox.Show(createlines);
                        MessageBox.Show(steplines);

                        File.WriteAllText(Application.StartupPath+"\\temp\\" + rs.name + ".lua",string.Format(objtemplate,rs.name,i.ToString(),createlines,steplines));
                    }
                    else if (rs.GetType() == typeof(Resources.Room))
                    {
                        roomrequires += Environment.NewLine + "\tlocal rm = require('" + rs.name + "')" + Environment.NewLine + "\ttable.insert(rooms,rm())" + Environment.NewLine + "\t" + rs.name + ":create()";
                        rms.Add((Resources.Room)rs);
                    }
                    i++;
                }

                foreach (Resources.Room rm in rms)
                {
                    string rmobjs = "--Spawn objects";
                    i = 0;

                    foreach (int k in rm.objid)
                    {
                        MessageBox.Show("K: " + k.ToString());
                        MessageBox.Show("Name = " + ((Resources.Object)Resources.resources[k]).name + ", K = " + k.ToString() + " , Position: " + rm.objpos[i].ToString());
                        rmobjs += Environment.NewLine + "\tlocal obj = require('" + ((Resources.Object)Resources.resources[k]).name + "')" + Environment.NewLine + "\ttable.insert(self.objects,obj(" + rm.objpos[i].X + "," + rm.objpos[i].Y + "))";
                        i++;
                    }

                    File.WriteAllText(Application.StartupPath + "\\temp\\" + rm.name + ".lua", string.Format(roomtemplate, rm.name, rmobjs));
                }

                File.WriteAllText(Application.StartupPath + "\\temp\\main.lua", string.Format(maintemplate, roomrequires));
                File.Copy(Application.StartupPath + "\\libraries\\middleclass.lua", Application.StartupPath + "\\temp\\middleclass.lua");
            }
        }
    }
}
