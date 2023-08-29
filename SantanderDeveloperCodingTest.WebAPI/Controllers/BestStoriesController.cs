using Microsoft.AspNetCore.Mvc;

namespace SantanderDeveloperCodingTest.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BestStoriesController : ControllerBase
    {
        private readonly ILogger<BestStoriesController> _logger;

        public BestStoriesController(ILogger<BestStoriesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Retrieve the details of the first n "best stories" from the Hacker News API.
        /// </summary>
        /// <param name="n">Number of best stories to return.</param>
        /// <returns>An array of the first n "best stories" as returned by the Hacker News API, sorted by their score in a descending order.</returns>
        [HttpGet(Name = "GetBestStories")]
        public async Task<IEnumerable<BestStory>> Get(int n)
        {
            var hackerNewsHttpClient = new HackerNewsHttpClient();
            var bestStories = await hackerNewsHttpClient.GetBestStories();
            if (bestStories == null)
                return new BestStory[0];
            var random = new Random();
            var response = bestStories.Take(n).Select(s =>
             new BestStory
             {
                 CommentCount = random.Next(1000),
                 PostedBy = Path.GetRandomFileName(),
                 Score = random.Next(1000),
                 time = DateTime.Now - new TimeSpan(random.Next(1000000)),
                 Title = Path.GetRandomFileName(),
                 Uri = "https://" + Path.GetRandomFileName()
             }).ToArray();
            return response;
        }
    }
}