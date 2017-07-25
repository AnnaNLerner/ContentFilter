using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentFilter
{
    public abstract class AbstractReadFile
    {
        public void readFile(string path)
        {
            System.Console.WriteLine("------Start reading {0}-------",path);
            int counter = 0;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(path);

            while ((line = file.ReadLine()) != null)
            {
                Action(line);
            //    System.Console.WriteLine(line);
                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
            System.Console.WriteLine("---------------------------");
            
        }

        public abstract void Action(string sentence);
    }
}
