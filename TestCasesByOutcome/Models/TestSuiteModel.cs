using System;
using System.Collections.Generic;

namespace TestCasesByOutcome
{
    class TestSuiteModel { 
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public ProjectModel project { get; set; }
        public PlanModel plan { get; set; }
        public int revision { get; set; }
        public int testCaseCount { get; set; }
        public string suiteType { get; set; }
        public string testCasesUrl { get; set; }
        public bool inheritDefaultConfigurations { get; set; }
        public List<DefaultConfiguration> defaultConfigurations { get; set; }
        public string state { get; set; }
        public User lastUpdatedBy { get; set; }
        public DateTime lastUpdatedDate { get; set; }
    }

    public class DefaultConfiguration
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
