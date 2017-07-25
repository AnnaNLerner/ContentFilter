using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentFilter
{
    public class EndsWith_Rule : GeneralRule
    {
        override
        public GeneralRule initRule(KeyValuePair<string,string> rule)
       {
            string temp = "(.+)EndWith$";
            setName(rule.Key);
            if (rule.Value.ToLowerInvariant().Trim().StartsWith("endwith"))
            {
                string word = rule.Value.Replace("EndWith", string.Empty).Trim().Trim('"');

                _myPatternString = temp.Replace("EndWith", word);
                _myPattern = new Regex(_myPatternString, RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

                Console.WriteLine("{1} regex is:{0}",_myPatternString,_name);
            }
            return this;
       }
        
    }
}
