using System.ComponentModel.DataAnnotations;

namespace triggeredapi.Models
{
    public class NovelDto
    {
        public Guid Id {get; set;}
        public string Title {get; set;}
        public string Website {get; set;}
        public string Url {get; set;}
        public string Image {get; set;}
        public DateTime LastUpdate {get; set;}
        public int LatestChapter {get; set;}
    }
}