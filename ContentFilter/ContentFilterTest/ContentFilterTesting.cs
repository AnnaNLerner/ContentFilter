using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContentFilter;
using System.Collections.Generic;

namespace ContentFilterTest
{
    [TestClass]
    public class ContentFilterTesting
    {
        [TestMethod]
        public void ValidateActivationR7()
        {
            WordCount_Rule r7 = new WordCount_Rule();
            KeyValuePair<string, string> rule = new KeyValuePair<string, string>("R7", "WordCountEqual 12");
            r7.initRule(rule);
            string sentence = "Test-This is a test for 12 words with different a punctuation.";
            RuleResult r = r7.Activate(sentence);
            Assert.AreEqual(r.IsMatch(),true);
        }

        [TestMethod]
        public void ValidateActivationR1()
        {
            StartsWith_Rule r1 = new StartsWith_Rule();
            KeyValuePair<string, string> rule = new KeyValuePair<string, string>("R1", "BeginWith \"Hello\"");
            r1.initRule(rule);
            string sentence = "Hello -testing r1 rule - sentence begins with HELLO word.";
            Assert.AreEqual(r1.Activate(sentence).IsMatch(), true);
        }

    }
}
