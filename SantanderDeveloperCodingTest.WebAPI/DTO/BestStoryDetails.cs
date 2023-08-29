namespace SantanderDeveloperCodingTest.WebAPI.DTO
{
    public class BestStoryDetails
    {
        private static readonly DateTime UnixDateTimeStart = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public string By { get; set; }
        public int Descendants { get; set; }
        public int Id { get; set; }
        public int[] Kids { get; set; }
        public int Score { get; set; }
        public int Time { get; set; }
        public DateTime TimeAsDateTime => UnixDateTimeStart.AddSeconds(Time);
        public string Title { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
