using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PullRequestAPPAPI
{
    public class PullRequest
    {
        public async Task CreatePullRequest()
        {
            //GetCall();
            //updateFileAndCommit();
            string accessToken = "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";
            string owner = "idheerajcse";
            string repo = "GithubPullrequest";
            string baseBranch = "main";
            string headBranch = "feature101";
            string title = "New Feature";
            string body = "Adding a new feature";
            string fileName = "dhp/onboarding/swimlines/swimlane123.yml";
            string fileContent = "key:cmdb-app-service-id; Tags: new Tag ";
            string newFileContent = "key:cmdb-app-service-id-new Id";
            string commitMessage = "Update yml file"; // Commit message
            string branch = "feature101";


            using (HttpClient client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Add("Authorization", "token " + accessToken);

                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", accessToken);

                //Update a new file
                HttpResponseMessage getFileResponse = await client.GetAsync($"https://api.github.com/repos/{owner}/{repo}/contents/{fileName}?ref={branch}");
                var res = getFileResponse.EnsureSuccessStatusCode();
                string currentFileContent = await getFileResponse.Content.ReadAsStringAsync();

                // Decode the base64-encoded content
                byte[] data = Convert.FromBase64String(currentFileContent);
                string decodedFileContent = Encoding.UTF8.GetString(data);

                // Modify the file content
                string updatedFileContent = newFileContent;

                // Commit the changes
                var commitJson = $"{{\"message\": \"{commitMessage}\", \"content\": \"{Convert.ToBase64String(Encoding.UTF8.GetBytes(updatedFileContent))}\", \"sha\": {getFileResponse.Headers.ETag}, \"branch\": \"{branch}\"}}";
                var commitContent = new StringContent(commitJson, Encoding.UTF8, "application/json");
                HttpResponseMessage commitResponse = await client.PutAsync($"https://api.github.com/repos/{owner}/{repo}/contents/{fileName}", commitContent);
                commitResponse.EnsureSuccessStatusCode();

                Console.WriteLine("File updated successfully.");


                // Create a new file
                var createFileJson = $"{{\"message\": \"{title}\", \"content\": \"{Convert.ToBase64String(Encoding.UTF8.GetBytes(fileContent))}\", \"branch\": \"{headBranch}\"}}";
                var createFileContent = new StringContent(createFileJson, Encoding.UTF8, "application/json");
                HttpResponseMessage createFileResponse = await client.PutAsync($"https://api.github.com/repos/{owner}/{repo}/contents/{fileName}", createFileContent);

                if (!createFileResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error creating file: {createFileResponse.ReasonPhrase}");
                    return;
                }

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
    var token = "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";

    client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

    var response = await client.GetAsync("/user");

}

public static async Task updateFileAndCommit()
{

    string accessToken = "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";
    string owner = "idheerajcse";
    string repo = "GithubPullrequest";
    string branch = "feature101";
    string headBranch = "feature101";
    string title = "New Feature";
    string body = "Adding a new feature";
    string fileName = "dhp/onboarding/swimlines/swimlane123.yml";
    string newFileContent = "key:cmdb-app-service-id-test-update";
    string commitMessage = "Update yml file"; // Commit message

    using (HttpClient client = new HttpClient())
    {
        client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("AppName", "1.0"));
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", accessToken);
        // Get current file content
        HttpResponseMessage getFileResponse = await client.GetAsync($"https://api.github.com/repos/{owner}/{repo}/contents/{fileName}?ref={branch}");
        var res = getFileResponse.EnsureSuccessStatusCode();
        string currentFileContent = await getFileResponse.Content.ReadAsStringAsync();

        // Decode the base64-encoded content
        byte[] data = Convert.FromBase64String(currentFileContent);
        string decodedFileContent = Encoding.UTF8.GetString(data);

        // Modify the file content
        string updatedFileContent = newFileContent;

        // Commit the changes
        var commitJson = $"{{\"message\": \"{commitMessage}\", \"content\": \"{Convert.ToBase64String(Encoding.UTF8.GetBytes(updatedFileContent))}\", \"sha\": \"{getFileResponse.Headers.ETag}\", \"branch\": \"{branch}\"}}";
        var commitContent = new StringContent(commitJson, Encoding.UTF8, "application/json");
        HttpResponseMessage commitResponse = await client.PutAsync($"https://api.github.com/repos/{owner}/{repo}/contents/{fileName}", commitContent);
        commitResponse.EnsureSuccessStatusCode();

        Console.WriteLine("File updated successfully.");
    }
}
        
    



//static async Task Main(string[] args)
//{
//    string accessToken = "YOUR_GITHUB_ACCESS_TOKEN";
//    string owner = "REPO_OWNER";
//    string repo = "REPO_NAME";
//    string baseBranch = "master";
//    string headBranch = "feature-branch";
//    string title = "New Feature";
//    string body = "Adding a new feature";
//    string fileName = "example.txt";
//    string fileContent = "This is an example file.";

//    using (HttpClient client = new HttpClient())
//    {
//        client.DefaultRequestHeaders.Add("Authorization", "token " + accessToken);

//        // Create a new file
//        var createFileJson = $"{{\"message\": \"{title}\", \"content\": \"{Convert.ToBase64String(Encoding.UTF8.GetBytes(fileContent))}\", \"branch\": \"{headBranch}\"}}";
//        var createFileContent = new StringContent(createFileJson, Encoding.UTF8, "application/json");
//        HttpResponseMessage createFileResponse = await client.PutAsync($"https://api.github.com/repos/{owner}/{repo}/contents/{fileName}", createFileContent);

//        if (!createFileResponse.IsSuccessStatusCode)
//        {
//            Console.WriteLine($"Error creating file: {createFileResponse.ReasonPhrase}");
//            return;
//        }

//        // Create a pull request
//        var createPullRequestJson = $"{{\"title\": \"{title}\", \"body\": \"{body}\", \"head\": \"{headBranch}\", \"base\": \"{baseBranch}\"}}";
//        var createPullRequestContent = new StringContent(createPullRequestJson, Encoding.UTF8, "application/json");
//        HttpResponseMessage createPullRequestResponse = await client.PostAsync($"https://api.github.com/repos/{owner}/{repo}/pulls", createPullRequestContent);

//        if (createPullRequestResponse.IsSuccessStatusCode)
//        {
//            Console.WriteLine("Pull request created successfully.");
//        }
//        else
//        {
//            Console.WriteLine($"Error creating pull request: {createPullRequestResponse.ReasonPhrase}");
//        }
//    }


    }
}
