using System;

namespace TestCasesByOutcome
{
    public class User
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public Links _links { get; set; }
        public Guid id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }
}