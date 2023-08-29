namespace SantanderDeveloperCodingTest.WebAPI
{
    public class HackerNewsHttpClient
    {
        public async Task<int[]?> GetBestStories()
        {
            var httpClient = new HttpClient();
            var bestStories = await httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
            var response = await bestStories.Content.ReadFromJsonAsync<int[]>();
            return response;
        }
    }
}