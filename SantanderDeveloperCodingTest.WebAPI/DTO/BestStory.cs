namespace SantanderDeveloperCodingTest.WebAPI.DTO
{
    public class BestStory
    {
        public int CommentCount { get; set; }
        public string? PostedBy { get; set; }
        public int Score { get; set; }
        public DateTime Time { get; set; }
        public string? Title { get; set; }
        public string? Uri { get; set; }
    }
}