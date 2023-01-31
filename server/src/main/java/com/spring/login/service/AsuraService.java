package com.spring.login.service;

import com.spring.login.model.Title;
import com.spring.login.repository.TitleRepository;
import jakarta.json.Json;
import jakarta.json.JsonArray;
import jakarta.json.JsonArrayBuilder;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class AsuraService {
    @Autowired
    TitleRepository titleRepository;

    private String url = "https://asurascans.com";
    public Optional<List<Title>> searchSeries(String searchTerm)  {

        // user's searchterm returns a list of series to be returned to Angular as JsonArray
        // users will select the title which will be persisted into DB: Triggers(Id, Title)

        String search = searchTerm.trim().replaceAll("\\s", "+");
        String finalSearch = url + "/?s=" + searchTerm;

        try{
            Document doc = Jsoup.connect(finalSearch)
                    .userAgent("Mozilla")
                    .get();
            Elements e = doc.select( "div.bsx");
            List<Title> titles = new ArrayList<Title>();
            for(Element ea: e){
                String link = ea.select("a[href]").attr("abs:href");
                String title =  ea.select("a[href][title]").attr("title");
                String imgSrc=  ea.select("img[src]").attr("abs:src");
                titles.add(new Title(title, link, title, imgSrc, null, 0, null));
            }

            return Optional.of(titles);

        } catch(IOException e){
            return Optional.empty();
        }
    }

    // public Optional<Object> scrapeByTitle(String siteUrl, String title) throws NullPointerException {

    //     //Spring will schedule CRON job to search using searchTerm saved in DB

    //     try{

    //         Document doc = Jsoup.connect(siteUrl)
    //                 .userAgent("Mozilla")
    //                 .get();
    //         Element result = doc.select("ul.clstyle").first();
    //         String resultLink = result.select("a[href]").attr("href");
    //         String latestChapter = result.select("a span.chapternum").first().text();
    //         String dateUpdated = result.select("a span.chapterdate").first().text();
    //         SimpleDateFormat formatter = new SimpleDateFormat("MMMM dd, yyyy");
    //         java.util.Date chapterDate = formatter.parse(dateUpdated);
    //         java.sql.Date sqlDate = new java.sql.Date(chapterDate.getTime());

    //         return Optional.of(new Title(title, resultLink, "Asura", "", sqlDate, latestChapter));

    //     }catch(NullPointerException | IOException e){
    //         System.out.println("something went wrong >>>>>>>>>");
    //         e.printStackTrace();

    //         return Optional.empty();

    //     } catch (ParseException e) {
    //         e.printStackTrace();
    //     }
    //     return Optional.empty();

    // }

}
