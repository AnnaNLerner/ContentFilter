using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContentFilter
{
    public class ContentManager
    {
        private static ContentManager _instance = new ContentManager();
        public static HashSet<GeneralRule> _rules = new HashSet<GeneralRule>();
        public static Dictionary<string, Type> _ruleTypes = new Dictionary<string, Type>();

        public static string _txtFilePath = System.Configuration.ConfigurationSettings.AppSettings["TextFilePath"];
        public static string _ruesFilePath = System.Configuration.ConfigurationSettings.AppSettings["RulesFilePath"];
        public static string _resultsFilePath = System.Configuration.ConfigurationSettings.AppSettings["ResultsFilePath"];
        public static ContentManager GetInstance()
        {
            if (_instance == null)
                return new ContentManager();
            return _instance;
        }

        public void InitService()
        {
            initRules();

            ReadRulesFile rules = new ReadRulesFile();
            rules.readFile(ContentManager._ruesFilePath);

            ReadContentFile content = new ReadContentFile();
            content.readFile(ContentManager._txtFilePath);



        }

        private void initRules()
        {
            _ruleTypes.Add("R1", typeof(StartsWith_Rule));
            _ruleTypes.Add("R2", typeof(EndsWith_Rule));
            _ruleTypes.Add("R3", typeof(ContainWord_Rule));
            _ruleTypes.Add("R4", typeof(AND_Rule));
            _ruleTypes.Add("R5", typeof(NOT_Rule));
            _ruleTypes.Add("R6", typeof(OR_Rule));
            _ruleTypes.Add("R7", typeof(WordCount_Rule));
        }
        
        
    }
}
