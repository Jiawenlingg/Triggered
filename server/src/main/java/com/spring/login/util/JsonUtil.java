package com.spring.login.util;

import jakarta.json.Json;
import jakarta.json.JsonObject;

public class JsonUtil {
    public static JsonObject seriesToJson(String title, String imgSrc, String link){
        return Json.createObjectBuilder()
                .add("title", title)
                .add("imgSrc", imgSrc)
                .add("link", link)
                .build();
    }
}
