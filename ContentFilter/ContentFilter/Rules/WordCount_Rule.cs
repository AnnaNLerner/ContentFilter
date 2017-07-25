using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentFilter
{
    public class WordCount_Rule : GeneralRule
    {
        public int _wordsCount = 0;

        override
        public GeneralRule initRule(KeyValuePair<string,string> rule)
       {
            string temp = @"((^WordCountEqual.)([0-9]+))";
            Regex tempR = new Regex(temp, RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            setName(rule.Key);
            if (rule.Value.ToLowerInvariant().Trim().StartsWith("wordcountequal"))
            {
                int count = Int32.Parse(tempR.Split(rule.Value)[3]);
                _myPatternString = @"(\b\w+\b)";
                _myPattern = new Regex(_myPatternString, RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
                _expectedMatches = count;
                Console.WriteLine("{1} regex is:{0}", _myPatternString, _name);
            }
            return this;

        }
        public override RuleResult Activate(string sentence)
        {
            _wordsCount = 0;
            RuleResult rr = new RuleResult(sentence);
            _wordsCount = _myPattern.Matches(sentence).Count;
            
            rr.SetIsMatch(_wordsCount == _expectedMatches ? true : false);
            return rr;
        }


    }
}
