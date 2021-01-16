using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EliteDockingLCDCore
{
    class VersionCheck
    {
        private static async Task<string> GetOnlineVersion()
        {
            using var client = new HttpClient();
            try
            {
                client.BaseAddress = new Uri("https://api.github.com");
                client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                var url = "repos/nolantern/elitedockinglcd/releases/latest";

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();

                dynamic jsonResponse = JsonConvert.DeserializeObject(resp);
                return jsonResponse.name;
            }
            catch (Exception)
            {
                return "-1";
            }
        }

        private static string GetVersion()
        {
            /*
             * shorten the version number from x.x.x.x to x.x.x
             * To make it comparable with online version
             */
            return Assembly.GetExecutingAssembly().GetName().Version.ToString().Remove(5);
        }

        public static async Task<bool> UpdateAvailabe()
        {
            string online = await GetOnlineVersion();
            if (online == "-1")
                return false;
            return online != GetVersion();
        }
    }
}
