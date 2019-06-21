using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace TestCasesByOutcome
{
    public class RestClientHelper
    {
        private string _token;

        public RestClientHelper(string token)
        {
            _token = token;
        }

        public async Task<T> Execute<T>(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($":{_token}")));

                    using (var response = await client.GetAsync(url))
                    {
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var convertedVal = JsonConvert.DeserializeObject<T>(responseBody);
                        return convertedVal;
                    }
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }

        }
    }
}

