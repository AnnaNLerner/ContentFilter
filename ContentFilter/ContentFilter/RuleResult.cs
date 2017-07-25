using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentFilter
{
    public class RuleResult
    {
        private bool _isMatch = false;
        private string _sentence = String.Empty;
        public RuleResult(string sentence)
        {
            _sentence = sentence;
        }
        public string Sentence
        {
            get
            {
                return _sentence;
            }
        }
        public bool IsMatch()
        {
            return _isMatch;
        }
        public void SetIsMatch(bool match)
        {
            _isMatch = match;
        }

        public void WriteResults(string ruleName)
        {
            if (IsMatch())
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Write("Activated rule {0}:", ruleName);
                Console.WriteLine(_sentence);
                Console.BackgroundColor = ConsoleColor.Black;

                WriteTextFile.SetRowsBlockToWrite(ContentManager._resultsFilePath, ruleName + " activated on: " + Sentence);
            }
        }





    }
}
