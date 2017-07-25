using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentFilter
{
    class WriteTextFile 
    {
        private static string _filePath = ContentManager._resultsFilePath;
        private ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();
        private const int _rowsPerWritingBlock = 10;
        public static List<string> _rowsBlock = new List<string>();
        static StringBuilder _sb = new StringBuilder();
        public static void SetRowsBlockToWrite(string path, string text)
        {
            _sb.AppendLine(text);
            WriteBlock();                
        }

        private static void WriteBlock()
        {
            Task t =  WriteToFile(_sb);
        }
        private static Object locker = new Object();

        public static async Task WriteToFile(StringBuilder text)
        {
            int timeOut = 2;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true)
            {
                try
                {
                    //Wait for resource to be free
                    lock (locker)
                    {
                        using (FileStream file = new FileStream(_filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                        using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                        {
                            writer.Write(text.ToString());
                        }
                    }
                    break;
                }
                catch
                {
                    throw new Exception("no free resource to write the data to file");
                }
            }
            if (stopwatch.ElapsedMilliseconds > timeOut)
            {
                string excpt = "no free resource to write the data to file. ElapsedMilliseconds (" + stopwatch.ElapsedMilliseconds + ") >ElapsedMilliseconds (" + timeOut + ")";
                throw new Exception(excpt);
            }
            await Task.Delay(5);
            stopwatch.Stop();
            _rowsBlock.Clear();

        }
    }
}
