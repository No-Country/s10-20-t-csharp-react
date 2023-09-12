namespace s10.Back.Services.Media
{
    public class VideoMetaData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string[] Tags { get; set; }
        public int Category { get; set; }
        public string Languaje { get; set; }
        public DateTime RecordedDate { get; set; }
        public string Location { get; set;}
    }
}