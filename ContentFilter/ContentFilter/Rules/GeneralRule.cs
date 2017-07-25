using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentFilter
{
    public abstract class GeneralRule
    {
        public delegate void ValidationDone(RuleResult result);
        public string _myPatternString;
        public Regex _myPattern;
        public bool _applyThis = false;
        public string _name = "RX";
        //private bool _isMatch = false;
        public int _expectedMatches = 1;
        public virtual GeneralRule initRule(KeyValuePair<string, string> rule) { return this; }
        public void setName(string name)
        {
            _name = name;
        }
        public bool isApply()
        {
            return _applyThis;
        }

        public virtual RuleResult Activate(string sentence)
        {
            RuleResult rr = new RuleResult(sentence);
            rr.SetIsMatch(_myPattern.IsMatch(sentence));
            return rr;
        }

        public void Done(RuleResult rr)
        {
            if (rr.IsMatch())  rr.WriteResults(_name);
        }
        public virtual GeneralRule Clone()
        {
            return (GeneralRule)MemberwiseClone();
        }

        public static GeneralRule GenerateRule(KeyValuePair<string, string> rule)
        {
            Type RXType = ContentManager._ruleTypes[rule.Key];
            object ruleInstance = Activator.CreateInstance(RXType);
            GeneralRule ruleObj = (GeneralRule)RXType.InvokeMember("initRule",
                BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, ruleInstance, new object[] { rule });
            return ruleObj;

        }
    }
}
