using HtmlAgilityPack;
using triggeredapi.Models;
using System.Globalization;

namespace triggeredapi.Service
{
    public enum NovelSite{
        mangago,
        asurascans,
        novelupdate
    }
    public class NovelParser
    {
        public NovelResult GetNovel(NovelSite site, string title){
            switch (site)
            {
                case NovelSite.mangago: return MangagoParser(title);
                case NovelSite.asurascans: return AsuraParser(title);
                case NovelSite.novelupdate: return NovelUpdateParser(title);
                default: throw new Exception ("Error: no such parser");
            }
        }
        public NovelResult MangagoParser(string title)
        {
            var html = $@"https://www.mangago.me/read-manga/{title.Replace(" ", "_")}/";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var novel_title = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='w-title']/h1").InnerText.Trim('\r', '\n', '\t');
            var latest = htmlDoc.DocumentNode.SelectSingleNode("//table[@id='chapter_table']/tbody/tr[1]");
            var latestUpdate = latest.SelectSingleNode("./td[last()]").InnerText.Trim('\r', '\n', '\t');
            var latestChapter = latest.SelectSingleNode("./td[1]/h4/a/b").InnerText.Trim('\r', '\n', '\t').Replace("Ch.", "");
            DateTime.TryParse(latestUpdate, out DateTime dt);
            int.TryParse(latestChapter, out int chapter);
            var result = new NovelResult(){
                LastUpdate = dt,
                LatestChapter = chapter,
                Title = novel_title,
                Website = "Mangago"
            };
            Console.WriteLine(result);
            return result;
        }

        public NovelResult NovelUpdateParser(string title)
        {
             var html = $@"https://www.novelupdates.com/series/{title.Replace("_","-")}/";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var novel_title = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='seriestitlenu']").InnerText.Trim('\r', '\n', '\t');
            var entry = htmlDoc.DocumentNode.SelectSingleNode("//table[@id='myTable']//tbody");
            var latestUpdate = entry.SelectSingleNode("./tr[0]").InnerText.Trim('\r', '\n', '\t');
            var latestChapter = entry.SelectSingleNode("./tr[2]/a[0]").InnerText.Trim('\r', '\n', '\t');
            DateTime.TryParse(latestUpdate, out DateTime dt);
            int.TryParse(latestChapter, out int chapter);
            var result = new NovelResult(){
                LastUpdate = dt,
                LatestChapter = chapter,
                Title = novel_title,
                Website = "Asura Scan"
            };
            Console.WriteLine(result);
            return result;
        }

         public NovelResult AsuraParser(string title)
        {
            var html = $@"https://www.asurascans.com/{title.Replace("_","-")}/";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var entry = htmlDoc.DocumentNode.SelectSingleNode("//h1[@class='entry-title']").InnerText.Trim('\r', '\n', '\t');
            var novel_title = string.Concat(entry.Take(entry.IndexOf("Chapter")-1));
            var latestUpdate = htmlDoc.DocumentNode.SelectSingleNode("//time[@class='entry-date']").InnerText.Trim('\r', '\n', '\t');
            var latestChapter = entry.Split("Chapter").Last().Trim();
            DateTime.TryParse(latestUpdate, out DateTime dt);
            int.TryParse(latestChapter, out int chapter);
            var result = new NovelResult(){
                LastUpdate = dt,
                LatestChapter = chapter,
                Title = novel_title,
                Website = "Asura Scan"
            };
            Console.WriteLine(result);
            return result;
        }
    }
}