using System;
using System.Collections.Generic;

namespace TestCasesByOutcome
{
    public class RunStatistic
    {
        public string state { get; set; }
        public string outcome { get; set; }
        public int count { get; set; }
    }

    internal class TestRunModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public bool isAutomated { get; set; }
        public string iteration { get; set; }
        public User owner { get; set; }
        public ProjectModel project { get; set; }
        public DateTime startedDate { get; set; }
        public DateTime completedDate { get; set; }
        public string state { get; set; }
        public PlanModel plan { get; set; }
        public string postProcessState { get; set; }
        public int totalTests { get; set; }
        public int incompleteTests { get; set; }
        public int notApplicableTests { get; set; }
        public int passedTests { get; set; }
        public int unanalyzedTests { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public User lastUpdatedBy { get; set; }
        public int revision { get; set; }
        public List<RunStatistic> runStatistics { get; set; }
        public string webAccessUrl { get; set; }
    }
}