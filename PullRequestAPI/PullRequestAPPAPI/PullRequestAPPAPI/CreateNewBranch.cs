using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PullRequestAPPAPI
{
    class CreateNewBranch
    {
       
        static async Task Main(string[] args)
        {
            string accessToken = "YOUR_GITHUB_ACCESS_TOKEN";
            string owner = "REPO_OWNER";
            string repo = "REPO_NAME";
            string baseBranch = "main"; // Specify the base branch from which you want to create the new branch
            string newBranchName = "new-branch"; // Specify the name of the new branch you want to create
            string baseBranchRef = $"refs/heads/{baseBranch}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "token " + accessToken);

                // Get the latest commit SHA of the base branch
                HttpResponseMessage getLatestCommitResponse = await client.GetAsync($"https://api.github.com/repos/{owner}/{repo}/git/refs/heads/{baseBranch}");
                getLatestCommitResponse.EnsureSuccessStatusCode();
                string latestCommitSha = (await getLatestCommitResponse.Content.ReadAsStringAsync())/*["object"]["sha"]*/;

                

                // Create the new branch
                var createBranchJson = $"{{\"ref\": \"refs/heads/{newBranchName}\", \"sha\": \"{latestCommitSha}\"}}";
                var createBranchContent = new StringContent(createBranchJson, Encoding.UTF8, "application/json");
                HttpResponseMessage createBranchResponse = await client.PostAsync($"https://api.github.com/repos/{owner}/{repo}/git/refs", createBranchContent);

                if (createBranchResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"New branch '{newBranchName}' created successfully.");
                }
                else
                {
                    Console.WriteLine($"Error creating branch: {createBranchResponse.ReasonPhrase}");
                }
            }
        }
    }
}

