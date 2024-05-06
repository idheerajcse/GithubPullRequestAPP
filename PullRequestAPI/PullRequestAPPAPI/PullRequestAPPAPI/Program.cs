using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PullRequestAPPAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //GetCall();
            string accessToken = "add token here";
            string owner = "idheerajcse";
            string repo = "GithubPullrequest";
            string baseBranch = "main";
            string headBranch = "feature101";
            string title = "New Feature";
            string body = "Adding a new feature";

            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Authorization", "token " + accessToken);
                #region adding new file in Git
                // Create a new file
                var createFileJson = $"{{\"message\": \"{title}\", \"content\": \"{Convert.ToBase64String(Encoding.UTF8.GetBytes(fileContent))}\", \"branch\": \"{headBranch}\"}}";
                var createFileContent = new StringContent(createFileJson, Encoding.UTF8, "application/json");
                HttpResponseMessage createFileResponse = await client.PutAsync($"https://api.github.com/repos/{owner}/{repo}/contents/{fileName}", createFileContent);

                if (!createFileResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error creating file: {createFileResponse.ReasonPhrase}");
                    return;
                }
                #endregion


                #region Create a Pull Request
                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", accessToken);

                var json = $"{{\"title\": \"{title}\", \"body\": \"{body}\", \"head\": \"{headBranch}\", \"base\": \"{baseBranch}\"}}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"https://api.github.com/repos/{owner}/{repo}/pulls", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Pull request created successfully.");
                }
                else
                {
                    Console.WriteLine($"Error creating pull request: {response.ReasonPhrase}");
                }
                #endregion
            }
        }

        private static void GetCall()
        {
            Task.WaitAll(ExecuteAsync());
            Console.ReadLine();
        }
        public static async Task ExecuteAsync()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com");
            var token = "Token here";

            client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

            var response = await client.GetAsync("/user");

        }
    }
}
