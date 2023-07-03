namespace triggeredapi.Models
{
    public class NovelResult
    {
        public string Title {get; set;}
        public string Website {get; set;}
        public string Url {get; set;}
        public DateTime LastUpdate {get; set;}
        public int LatestChapter {get; set;}

        public override string? ToString()
        {
            return $"{Title}: {LastUpdate} - {LatestChapter}";
        }
    }
}