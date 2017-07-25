using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentFilter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ContentManager mng = new ContentManager();
            mng.InitService();

            Console.ReadKey();
        }
    }
}
