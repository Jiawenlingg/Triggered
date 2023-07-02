using triggeredapi.Models;

namespace triggeredapi.Service
{
    public class NovelParser
    {
        public Func<string, NovelResult> GetParser(string site){
            switch (site)
            {
                case "MangaGo": return MangagoParser;
                case "AsuraScans": return AsuraParser;
                case "NovelUpdate": return NovelUpdateParser;
                default: throw new Exception ("Error: no such parser");
            }
        }
        public NovelResult MangagoParser(string title)
        {
            return new NovelResult();
        }

         public NovelResult AsuraParser(string title)
        {
            return new NovelResult();
        }

         public NovelResult NovelUpdateParser(string title)
        {
            return new NovelResult();
        }

    
        public Func<string, NovelResult> AsuraScans;
        public Func<string, NovelResult> MangagoParser;

    }
}