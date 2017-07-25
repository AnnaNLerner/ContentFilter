using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentFilter
{
    public class AND_Rule : GeneralRule
    {

        public HashSet<GeneralRule> _andRules = new HashSet<GeneralRule>();
        public string[] rules = new string[] { };
        public int _matchesCount = 0;
        override
        public GeneralRule initRule(KeyValuePair<string,string> rule)
        {
            string mulRules = @"(^AND!)?(R[\d]+)";
 
            Regex dependencies = new Regex(mulRules, RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase); 

            setName(rule.Key);
            if (rule.Value.ToLowerInvariant().Trim().StartsWith("and"))
            {
                Console.WriteLine(rule.Value);
                string[]  match = dependencies.Split(rule.Value).Where(s => !String.IsNullOrWhiteSpace(s)).ToArray<string>();
                if (match.Length>0)
                {
                    string[] rules = new string[match.Length]; //IGNORE FIRST MATCH OF "AND"

                    for (int i=0;i<rules.Length;i++)
                    {
                        rules[i] = match[i];

                        foreach(GeneralRule r in ContentManager._rules)
                        {
                            if (r._name.ToLower().Equals(rules[i].ToLower().Trim()))
                            {
                                _andRules.Add(r);
                            }
                        }
                    }
                    _expectedMatches = _andRules.Count;
                }
            }
            return this;
       }
        public override RuleResult Activate(string sentence)
        {
            _matchesCount = 0;
            RuleResult rr = new RuleResult(sentence);

            foreach (GeneralRule rx in _andRules)
            {
                GeneralRule r = rx.Clone();
                rr = r.Activate(sentence);
                if (rr.IsMatch()) _matchesCount++;
            }
            rr.SetIsMatch(_matchesCount == _expectedMatches ? true : false);
            return rr;
        }



    }
}
