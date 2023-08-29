using SantanderDeveloperCodingTest.WebAPI.DTO;

namespace SantanderDeveloperCodingTest.WebAPI
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
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var bestStories = await httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
                var response = await bestStories.Content.ReadFromJsonAsync<int[]>();
                return response;
            }
        }

        public async Task<BestStoryDetails?> GetBestStoryDetailsAsync(int id)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var uri = $"https://hacker-news.firebaseio.com/v0/item/{id}.json";
                var bestStories = await httpClient.GetAsync(uri);
                var response = await bestStories.Content.ReadFromJsonAsync<BestStoryDetails>();
                return response;
            }
        }
    }
}