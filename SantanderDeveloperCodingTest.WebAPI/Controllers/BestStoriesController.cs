using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SantanderDeveloperCodingTest.HackerNews;
using SantanderDeveloperCodingTest.HackerNews.DTO;
using SantanderDeveloperCodingTest.WebAPI.DTO;

namespace SantanderDeveloperCodingTest.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestStoriesController : ControllerBase
    {
        private static readonly MemoryCacheEntryOptions _cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1)).SetSize(1);
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BestStoriesController> _logger;
        private readonly IMemoryCache _memoryCache;
        public BestStoriesController(ILogger<BestStoriesController> logger, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        /// <summary>
        /// Retrieve the details of the first n "best stories" from the Hacker News API.
        /// </summary>
        /// <param name="n">Number of best stories to return.</param>
        /// <returns>An array of the first n "best stories" as returned by the Hacker News API, sorted by their score in a descending order.</returns>
        [HttpGet(Name = "GetBestStories")]
        public async Task<IEnumerable<BestStory?>> Get(int n)
        {
            // https://github.com/HackerNews/API/tree/master#new-top-and-best-stories maximum 500 best stories
            if (n <= 0 || n > 500)
                throw new ArgumentOutOfRangeException(nameof(n));

            var hackerNewsHttpClient = new HackerNewsService(_httpClientFactory);
            var bestStories = await hackerNewsHttpClient.GetBestStoriesAsync();
            if (bestStories == null)
                return Array.Empty<BestStory>();
            await InvalidateCachedItemsAsync(hackerNewsHttpClient);
            var getDetailsForAllStories = bestStories.Take(n).Select(async id => await GetBestStoryDetailsAsync(hackerNewsHttpClient, id));
            var response = await Task.WhenAll(getDetailsForAllStories);
            var sortedResponse = response.OrderByDescending(s => s?.Score).ToArray();
            return response;
        }

        private async Task<BestStory?> GetBestStoryDetailsAsync(HackerNewsService hackerNewsHttpClient, int id)
        {
            BestStoryDetails? bestStoryDetails;
            if (!_memoryCache.TryGetValue(id, out bestStoryDetails))
            {
                bestStoryDetails = await hackerNewsHttpClient.GetBestStoryDetailsAsync(id);
                _memoryCache.Set(id, bestStoryDetails, _cacheEntryOptions);
            }

            if (bestStoryDetails == null)
                return null;

            var bestStory = new BestStory
            {
                CommentCount = (bestStoryDetails.Kids == null || bestStoryDetails.Kids.Length <= 0) ? 0 : bestStoryDetails.Kids.Length,
                PostedBy = bestStoryDetails.By,
                Score = bestStoryDetails.Score,
                Time = bestStoryDetails.TimeAsDateTime,
                Title = bestStoryDetails.Title,
                Uri = bestStoryDetails.Url
            };
            return bestStory;
        }

        private async Task InvalidateCachedItemsAsync(HackerNewsService hackerNewsHttpClient)
        {
            var ids = await hackerNewsHttpClient.GetUpdatedItems();
            if (ids != null)
                foreach (var id in ids)
                    _memoryCache.Remove(id);
        }
    }
}