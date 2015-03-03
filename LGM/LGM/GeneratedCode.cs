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

            codelines.Add("--File generated using Love Game Maker (https://github.com/Radfordhound/love2d-game-maker)\n"
                    + "--It's not recommended to edit this file in Notepad (As it doesn't support " + @"\n" + " line breaks.)\n");

            codelines.Add("objects = {}\n"
                            + "objcount = 0\n");

            List<Resources.Object> objs = new List<Resources.Object>();
            int i = 0;

            foreach (Resources.Types rs in Resources.resources)
            {
                if (rs.GetType() == typeof(Resources.Object))
                {
                    //The resource is an object.
                    Resources.Object obj = (Resources.Object)rs;
                    objs.Add(obj);

                    //Define all the event lists
                    List<string> createlines = new List<string>();
                    List<string> steplines = new List<string>();

                    List<string> objlines = new List<string>();
                    objlines.Add(obj.name + " = {id = " + i.ToString() + ", x = 0, y = 0}\n");
                    objlines.Add("objects[objcount] = " + obj.name+'\n');
                    objlines.Add("objcount = objcount+1");

                    foreach (Actions.Types action in obj.actions)
                    {
                        if (action.eventtype == 0)
                        {
                            MessageBox.Show("Create event");
                            createlines.Add(generateAction(action.id,action,obj));
                        }
                        else if (action.eventtype == 1)
                        {
                            MessageBox.Show("Step event");
                            steplines.Add(generateAction(action.id,action,obj));
                        }
                    }

                    objlines.Add("function " + obj.name + ".create()");

                    foreach (string line in createlines)
                    {
                        objlines.Add('\t' + line);
                    } 
                    objlines.Add("end\n");

                    objlines.Add("function " + obj.name + ".step()");
                    foreach (string line in steplines)
                    {
                        objlines.Add('\t' + line);
                    }
                    objlines.Add("end\n");

                    File.WriteAllLines(Application.StartupPath + "\\temp\\" + obj.name + ".lua", objlines);
                    codelines.Add("require('" + obj.name + "')");
                }
                i++;
            }

            codelines.Add("\nfunction love.load()\n"
                            + "--The 'Game Start' event");

            foreach (Resources.Object obj in objs)
            {
                codelines.Add("\t" + obj.name + ".create()");
            }

            codelines.Add("end\n");

            codelines.Add("\nfunction love.update()\n"
                            + "--The 'Game Step' event");

            foreach (Resources.Object obj in objs)
            {
                codelines.Add("\t" + obj.name + ".step()");
            }

            codelines.Add("end\n");

            if (objs.Count > 0)
            {
                codelines.Add("function love.draw()\n"
                + "\tfor i=0," + (objs.Count-1).ToString() + " do\n"
                + "\t\tlove.graphics.print(" + '"' + "X: " + '"' + " ..objects[i].x .." + '"' + ", Y: " + '"' + " ..objects[i].y,0,i*16)\n"
                + "\tend\n"
                + "end");
            }

            File.WriteAllLines(Application.StartupPath + "\\temp\\main.lua",codelines);
        }

        public static string generateAction(int id,Actions.Types action,Resources.Object obj)
        {
            if (id == 0)
            {
                //Move to position
                return (obj.name + ".x = " + ((Actions.Move)action).x + "\n\t"
                        + obj.name + ".y = " + ((Actions.Move)action).y);
            }
            else if (id == 1)
            {
                //Create instance
                Resources.Object othobj = (Resources.Object)Resources.resources[((Actions.Createid)action).id];
                return ("objects[objcount+1] = " + obj.name + "\n\t"
                        + othobj.name + ".x = " + ((Actions.Createid)action).x + "\n\t"
                        + othobj.name + ".y = " + ((Actions.Createid)action).y);
            }
            return "";
        }
    }
}
