using Microsoft.AspNetCore.Mvc;
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
            var hackerNewsHttpClient = new HackerNewsHttpClient(_httpClientFactory);
            var bestStories = await hackerNewsHttpClient.GetBestStoriesAsync();
            if (bestStories == null)
                return new BestStory[0];
            var getDetailsForAllStories = bestStories.Take(n).Select(async id => await GetBestStoryDetailsAsync(hackerNewsHttpClient, id));
            var response = await Task.WhenAll(getDetailsForAllStories);
            return response;
        }

        private async Task<BestStory> GetBestStoryDetailsAsync(HackerNewsHttpClient hackerNewsHttpClient, int id)
        {
            var random = new Random();
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