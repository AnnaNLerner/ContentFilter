using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentFilter
{
    public class ReadRulesFile : AbstractReadFile
    {
        private string _rulesPattern = @"(R[1-9]+):(?<rule>.*)";
        private string _applyPattern = @"(APPLY)(?<appply>.*)";
        /*
        public delegate void InitRuleCallBack(KeyValuePair<string, string> rule);
        private void CreateRule(InitRuleCallBack callback, KeyValuePair<string, string> ruleString)
        {
            callback(ruleString);
        }
        private void CreateRuleObject(KeyValuePair<string, string> match)
        {
            StartsWith_Rule r1 = new StartsWith_Rule();
            //_rules.Add(r1.initRule(match));
        }
        */
        
        public override void Action(string line)
        {
            initRules(line);
        }

        private void initRules(string rule)
        {
            Regex p = new Regex(_rulesPattern, RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string[] rulesCol = p.Split(rule);
            if (rulesCol.Count() < 2) apppliedRule(rule);
            else
            {
                KeyValuePair<string, string> match = new KeyValuePair<string, string>(rulesCol[1].Trim(), rulesCol[2].Trim());
                ContentManager._rules.Add(GeneralRule.GenerateRule(match));
            }
        }

       

        private void apppliedRule(string text)
        {
            Regex p = new Regex(_applyPattern, RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.IgnoreCase);
            string[] ruleToApply = p.Split(text);
            System.Console.WriteLine("Applied rule:{0}", ruleToApply[2].Trim());
            foreach (GeneralRule r in ContentManager._rules)
            {
                if (r._name.Trim().ToLower() == ruleToApply[2].Trim().ToLower())
                    r._applyThis = true;
            }
        }
  

    }
}
