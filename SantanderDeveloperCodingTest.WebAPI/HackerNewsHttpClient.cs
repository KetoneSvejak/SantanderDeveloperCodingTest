using SantanderDeveloperCodingTest.WebAPI.DTO;

namespace SantanderDeveloperCodingTest.WebAPI
{
    public class HackerNewsHttpClient
    {
        public async Task<int[]?> GetBestStoriesAsync()
        {
            var httpClient = new HttpClient();
            var bestStories = await httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
            var response = await bestStories.Content.ReadFromJsonAsync<int[]>();
            return response;
        }

        public async Task<BestStoryDetails?> GetBestStoryDetailsAsync(int id)
        {
            var httpClient = new HttpClient();
            var uri = $"https://hacker-news.firebaseio.com/v0/item/{id}.json";
            var bestStories = await httpClient.GetAsync(uri);
            var response = await bestStories.Content.ReadFromJsonAsync<BestStoryDetails>();
            return response;
        }
    }
}