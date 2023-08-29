using SantanderDeveloperCodingTest.HackerNews.DTO;
using System.Net.Http.Json;

namespace SantanderDeveloperCodingTest.HackerNews
{
    public class HackerNewsService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HackerNewsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<int[]?> GetBestStoriesAsync()
        {
            using (var httpClient = CreateClient())
            {
                var bestStories = await httpClient.GetAsync("beststories.json");
                var response = await bestStories.Content.ReadFromJsonAsync<int[]>();
                return response;
            }
        }

        public async Task<BestStoryDetails?> GetBestStoryDetailsAsync(int id)
        {
            using (var httpClient = CreateClient())
            {
                var uri = $"item/{id}.json";
                var bestStories = await httpClient.GetAsync(uri);
                var response = await bestStories.Content.ReadFromJsonAsync<BestStoryDetails>();
                return response;
            }
        }

        /// <summary>
        /// Get ids of changed items.
        /// https://github.com/HackerNews/API/tree/master#changed-items-and-profiles . 
        /// </summary>
        /// <returns>ids of changed items</returns>
        public async Task<int[]?> GetUpdatedItems()
        {
            using (var httpClient = CreateClient())
            {
                var uri = $"updates.json";
                var bestStories = await httpClient.GetAsync(uri);
                var response = await bestStories.Content.ReadFromJsonAsync<UpdatesResponse>();
                return response?.Items;
            }
        }

        private HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient(nameof(HackerNewsService));
        }
    }
}