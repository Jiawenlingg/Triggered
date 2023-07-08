using System.ComponentModel.DataAnnotations;

namespace triggeredapi.Models
{
    public class NovelResult
    {
        [Required]
        public string Title {get; set;}

        [Required]
        public string Website {get; set;}

        [Required]
        public string Url {get; set;}
        public string Image {get; set;}
        public DateTime LastUpdate {get; set;}
        public int LatestChapter {get; set;}

        public override string? ToString()
        {
            return $"{Title}: {LastUpdate} - {LatestChapter}";
        }
    }
}