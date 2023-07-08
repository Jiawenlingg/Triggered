using HtmlAgilityPack;
using triggeredapi.Models;
using System.Globalization;

namespace triggeredapi.Service
{
    public class NovelParser
    {
        private Dictionary<string, string> urls = new Dictionary<string, string>(){
            {"www.mangago.me", "https://www.mangago.me/read-manga/"},
            {"www.novelupdates.com", "https://www.novelupdates.com/series/"},
            {"www.asurascans.com", "https://www.asurascans.com/"}
        };
        public NovelResult GetNovel(string url){
            try
            {
                var uri = new Uri(url);
                var domain = uri.Host;
                if(!urls.TryGetValue(domain, out string siteUrl)) 
                    throw new Exception("Not a supported website!");
                var title = uri.Segments.LastOrDefault();
                var finalUrl = siteUrl+title;
                // var mangagoUrl = $@"{title.Replace(" ", "_").Replace("'", "_")}/";
                // var novelUpdateUrl = $@"{title.Replace("_","-").Replace("'", "")}/";
                // var AsuraScansUrl = $@"{title.Replace("_","-").Replace("'", "")}/";
                switch (domain)
                {
                    case "www.mangago.me": return MangagoParser(finalUrl);
                    case "www.asurascans.com": return AsuraParser(finalUrl);
                    case "www.novelupdates.com": return NovelUpdateParser(finalUrl);
                    default: throw new Exception ("Error: no such parser");
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception($"Could not find novel: {ex}");
            }
        }
        public NovelResult MangagoParser(string url)
        {
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);

            var novel_title = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='w-title']/h1").InnerText.Trim('\r', '\n', '\t');
            var latest = htmlDoc.DocumentNode.SelectSingleNode("//table[@id='chapter_table']/tbody/tr[1]");
            var latestUpdate = latest.SelectSingleNode("./td[last()]").InnerText.Trim('\r', '\n', '\t');
            var latestChapter = latest.SelectSingleNode("./td[1]/h4/a/b").InnerText.Trim('\r', '\n', '\t').Replace("Ch.", "");
            var pic = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='left cover']/img").Attributes["src"].Value;
            DateTime.TryParse(latestUpdate, out DateTime dt);
            int.TryParse(latestChapter, out int chapter);
            var result = new NovelResult(){
                LastUpdate = dt,
                LatestChapter = chapter,
                Title = novel_title,
                Website = "Mangago",
                Url = url,
                Image = pic
            };
            Console.WriteLine(result);
            return result;
        }

        public NovelResult NovelUpdateParser(string url)
        {

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);

            var novel_title = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='seriestitlenu']").InnerText.Trim('\r', '\n', '\t');
            var entry = htmlDoc.DocumentNode.SelectSingleNode("//table[@id='myTable']/tbody/tr[1]");
            var latestChapter = entry.SelectSingleNode("./td[last()]/a[1]").InnerText.Trim('\r', '\n', '\t').Replace("c","");
            var latestUpdate = entry.SelectSingleNode("./td[1]").InnerText.Trim('\r', '\n', '\t');
            var pic = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='seriesimg']/img").Attributes["src"].Value;
            DateTime.TryParse(latestUpdate, out DateTime dt);
            int.TryParse(latestChapter, out int chapter);
            var result = new NovelResult(){
                LastUpdate = dt,
                LatestChapter = chapter,
                Title = novel_title,
                Website = "Novel Update",
                Url = url,
                Image = pic
            };
            Console.WriteLine(result);
            return result;
        }

         public NovelResult AsuraParser(string url)
        {
            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(url);

            var novel_title = htmlDoc.DocumentNode.SelectSingleNode("//h1[@class='entry-title']").InnerText.Trim('\r', '\n', '\t');
            var entry = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='chapterlist']/ul/li[1]");
            var latestChapter = entry.SelectSingleNode(".//span[@class='chapternum']").InnerText.Trim('\r', '\n', '\t').Replace("Chapter ", "");
            var latestUpdate = entry.SelectSingleNode(".//span[@class='chapterdate']").InnerText.Trim('\r', '\n', '\t');
            var pic = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='thumb']/img").Attributes["src"].Value;

            DateTime.TryParse(latestUpdate, out DateTime dt);
            int.TryParse(latestChapter, out int chapter);
            var result = new NovelResult(){
                LastUpdate = dt,
                LatestChapter = chapter,
                Title = novel_title,
                Website = "Asura Scan",
                Url = url,
                Image = pic
            };
            Console.WriteLine(result);
            return result;
        }
    }
}