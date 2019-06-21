using System;

namespace TestCasesByOutcome
{
    public class Configuration
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class TestPlanReference
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class TestSuiteReference
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class RunBy
    {
        public string displayName { get; set; }
        public string id { get; set; }
    }

    public class LastResultDetails
    {
        public int duration { get; set; }
        public DateTime dateCompleted { get; set; }
        public RunBy runBy { get; set; }
    }

    public class TestResults
    {
        public LastResultDetails lastResultDetails { get; set; }
        public int lastResultId { get; set; }
        public string lastRunBuildNumber { get; set; }
        public string state { get; set; }
        public string lastResultState { get; set; }
        public string outcome { get; set; }
        public string failureType { get; set; }
        public string lastResolutionState { get; set; }
        public int lastTestRunId { get; set; }
    }

    public class TestPointLinks
    {
        public Link _self { get; set; }
        public Link sourcePlan { get; set; }
        public Link sourceSuite { get; set; }
        public Link sourceProject { get; set; }
        public Link testCases { get; set; }
        public Link run { get; set; }
        public Link result { get; set; }
    }

    public class TestCaseReference
    {
        public int id { get; set; }
        public string name { get; set; }
        public string state { get; set; }
    }

    public class TestPointModel
    {
        public int id { get; set; }
        public User tester { get; set; }
        public Configuration configuration { get; set; }
        public bool isAutomated { get; set; }
        public ProjectModel project { get; set; }
        public TestPlanReference testPlan { get; set; }
        public TestSuiteReference testSuite { get; set; }
        public User lastUpdatedBy { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public TestResults results { get; set; }
        public DateTime lastResetToActive { get; set; }
        public bool isActive { get; set; }
        public TestPointLinks links { get; set; }
        public TestCaseReference testCaseReference { get; set; }
    }
}