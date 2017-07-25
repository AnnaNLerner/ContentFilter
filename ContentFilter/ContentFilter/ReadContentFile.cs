using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentFilter.GeneralRule;

namespace ContentFilter
{
    public class ReadContentFile : AbstractReadFile
    {
        private GeneralRule _appliedRule;
        public override void Action(string sentence)
        {
            foreach(GeneralRule r in ContentManager._rules)
            {
                if (r.isApply()) _appliedRule = r;
            }
            if (_appliedRule != null)
            {
                RuleResult rr = _appliedRule.Activate(sentence);
                _appliedRule.Done(rr);

            }
        }

    }
}
