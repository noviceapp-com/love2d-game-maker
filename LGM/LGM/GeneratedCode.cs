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
        public static List<string> codelines = new List<string>();

        public static void GenerateCode()
        {
            //Generate lua code for LOVE 2D
            codelines.Clear();

            List<Resources.Object> objs = new List<Resources.Object>();
            int i = 0;

            foreach (Resources.Types rs in Resources.resources)
            {
                if (rs.GetType() == typeof(Resources.Object))
                {
                    //The resource is an object.
                    Resources.Object obj = (Resources.Object)rs;
                    objs.Add(obj);
                    List<string> objlines = new List<string>();
                    objlines.Add("id = " + i.ToString());

                    foreach (int eventid in obj.events)
                    {
                        if (eventid == 0)
                        {
                            objlines.Add("function " + obj.name + "_Create()");
                            foreach (Actions.Types action in obj.actions)
                            {
                                if (action.eventid == 0)
                                {
                                    //The action is part of a create event
                                    if (action.id == 0)
                                    {
                                        objlines.Add("\tobjpos[id][0] = " + ((Actions.Move)action).x);
                                        objlines.Add("\tobjpos[id][1] = " + ((Actions.Move)action).y);
                                    }
                                }
                            }
                        }
                        objlines.Add("end");
                    }
                    File.WriteAllLines(Application.StartupPath + "\\temp\\" + obj.name + ".lua", objlines);
                    codelines.Add("require('" + obj.name+"')");
                    codelines.Add("");
                    codelines.Add("objpos = {}");
                    codelines.Add("");
                    codelines.Add("for i = 0, " + objs.Count.ToString() + " do");
                    codelines.Add("\tobjpos[i] = {}");
                    codelines.Add("\t");
                    codelines.Add("\tfor j = 0, 1 do");
                    codelines.Add("\t\tobjpos[i][j] = 0");
                    codelines.Add("\tend");
                    codelines.Add("end");
                }
                i++;
            }

            codelines.Add("");
            codelines.Add("function love.load()");
            foreach (Resources.Object obj in objs)
            {
                codelines.Add("\t" + obj.name + "_Create()");
            }
            codelines.Add("end");

            codelines.Add("");
            codelines.Add("function love.draw()");
            codelines.Add("\tlove.graphics.print(" + '"' + "X: " + '"' + " ..objpos[0][0] .." + '"' + ", Y: " + '"' + " ..objpos[0][1],0,0)");
            codelines.Add("end");

            File.WriteAllLines(Application.StartupPath + "\\temp\\main.lua",codelines);
        }
    }
}
