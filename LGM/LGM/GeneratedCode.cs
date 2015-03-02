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
                    //List<string> curlines = new List<string>();

                    List<string> objlines = new List<string>();
                    objlines.Add(obj.name + " = {id = " + i.ToString() + ", x = 0, y = 0}\n");

                    foreach (Actions.Types action in obj.actions)
                    {
                        if (action.eventid == 0)
                        {
                            MessageBox.Show("Create event");
                            createlines.Add(generateAction(action.id,action,obj));
                        }
                        else if (action.eventid == 1)
                        {
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

            codelines.Add("\nfunction love.load()");

            foreach (Resources.Object obj in objs)
            {
                codelines.Add("\t" + obj.name + ".create()");
            }

            codelines.Add("end\n\n"
                            + "function love.draw()\n"
                            + "\tlove.graphics.print(" + '"' + "X: " + '"' + " ..Object0.x .." + '"' + ", Y: " + '"' + " ..Object0.y,0,0)\n"
                            + "end");

            File.WriteAllLines(Application.StartupPath + "\\temp\\main.lua",codelines);
        }

        public static string generateAction(int id,Actions.Types action,Resources.Object obj)
        {
            if (id == 0)
            {
                return (obj.name + ".x = " + ((Actions.Move)action).x + "\n\t"
                        + obj.name + ".y = " + ((Actions.Move)action).y);
            }
            return "";
        }
    }
}
