using Microsoft.AspNetCore.Mvc;
using SantanderDeveloperCodingTest.HackerNews;
using SantanderDeveloperCodingTest.WebAPI.DTO;

namespace SantanderDeveloperCodingTest.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestStoriesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BestStoriesController> _logger;
        public BestStoriesController(ILogger<BestStoriesController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// Retrieve the details of the first n "best stories" from the Hacker News API.
        /// </summary>
        /// <param name="n">Number of best stories to return.</param>
        /// <returns>An array of the first n "best stories" as returned by the Hacker News API, sorted by their score in a descending order.</returns>
        [HttpGet(Name = "GetBestStories")]
        public async Task<IEnumerable<BestStory>> Get(int n)
        {
            // https://github.com/HackerNews/API/tree/master#new-top-and-best-stories maximum 500 best stories
            if (n <= 0 || n > 500)
                throw new ArgumentOutOfRangeException(nameof(n));

            var hackerNewsHttpClient = new HackerNewsService(_httpClientFactory);
            var bestStories = await hackerNewsHttpClient.GetBestStoriesAsync();
            if (bestStories == null)
                return Array.Empty<BestStory>();
            var getDetailsForAllStories = bestStories.Take(n).Select(async id => await GetBestStoryDetailsAsync(hackerNewsHttpClient, id));
            var response = await Task.WhenAll(getDetailsForAllStories);
            var sortedResponse = response.OrderByDescending(s => s.Score).ToArray();
            return response;
        }

        private static async Task<BestStory> GetBestStoryDetailsAsync(HackerNewsService hackerNewsHttpClient, int id)
        {
            var bestStoryDetails = await hackerNewsHttpClient.GetBestStoryDetailsAsync(id);
            if (bestStoryDetails == null)
                throw new ArgumentOutOfRangeException(nameof(id));
            var bestStory = new BestStory
            {
                CommentCount = (bestStoryDetails.Kids == null || bestStoryDetails.Kids.Length <= 0) ? 0 : bestStoryDetails.Kids.Length,
                PostedBy = bestStoryDetails.By,
                Score = bestStoryDetails.Score,
                time = bestStoryDetails.TimeAsDateTime,
                Title = bestStoryDetails.Title,
                Uri = bestStoryDetails.Url
            };
            return bestStory;
        }
    }
}