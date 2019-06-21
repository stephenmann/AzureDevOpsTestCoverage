using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.TeamFoundation.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestCasesByOutcome
{
    public static partial class GetCases
    {
        private static TraceWriter _log;
        private static RestClientHelper _helper;

        [FunctionName("GetCases")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            // parse query parameter
            var org = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "orginization", true) == 0).Value;
            var pat = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "token", true) == 0).Value;

            if(string.IsNullOrWhiteSpace(org) || string.IsNullOrWhiteSpace(pat))
            {
                return req.CreateErrorResponse(HttpStatusCode.BadRequest, "Missing required orginization and token parameters.");
            }

            _log = log;
            var result = new List<TestPointModel>();

            try
            {
                _helper = new RestClientHelper(pat);

                _log.Info($"Getting all projects for orginization {org}");
                var projects = await GetProjects(org);
                foreach (var p in projects)
                {
                    _log.Info($"Getting all test runs for project {p.Name}");
                    var runs = await GetTestRuns(org, p.Name);
                    var suites = new List<TestSuiteModel>();
                    int planid = 0;
                    foreach (var r in runs)
                    {
                        _log.Info($"Getting test details for run {r.id}");
                        var runDetail = await _helper.Execute<TestRunModel>(r.url);
                        if (runDetail.plan != null && planid != runDetail.plan.id)
                        {
                            _log.Info($"Getting test suites for plan {runDetail.plan.name}");
                            var sQueryResults = await _helper.Execute<RestClassHelper<TestSuiteModel[]>>($"https://dev.azure.com/{org}/{p.Name}/_apis/test/Plans/{runDetail.plan.id}/suites");
                            suites.AddRange(sQueryResults?.value);
                            planid = runDetail.plan.id;
                        }
                    }

                    _log.Info($"Getting all test plans for orginization {org}");
                    foreach (var s in suites)
                    {
                        var tpQueryResult = await _helper.Execute<RestClassHelper<TestPointModel[]>>($"https://dev.azure.com/{org}/{p.Name}/_apis/testplan/Plans/{s.plan.id}/Suites/{s.id}/TestPoint");
                        result.AddRange(tpQueryResult.value);
                    }
                }
                return req.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                _log.Error(ex.GetBaseException().Message);
                return req.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private static async Task<TestRunReference[]> GetTestRuns(string organization, string project)
        {
            var runs = await _helper.Execute<RestClassHelper<TestRunReference[]>>($"https://dev.azure.com/{organization}/{project}/_apis/test/runs");
            _log.Info($"Found {runs.count} test runs.");
            return runs.value ?? new TestRunReference[0];
        }

        public static async Task<TeamProjectReference[]> GetProjects(string organization)
        {
            var projects = await _helper.Execute<RestClassHelper<TeamProjectReference[]>>($"https://dev.azure.com/{organization}/_apis/projects");
            _log.Info($"Found {projects.count} projects.");
            return projects.value ?? new TeamProjectReference[0];
        }
    }
}

