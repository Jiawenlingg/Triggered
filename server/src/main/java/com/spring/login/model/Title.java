package com.spring.login.model;

import lombok.Data;

import java.sql.Date;
import java.util.HashSet;
import java.util.Set;

import javax.persistence.*;

@Data
@Entity
@Table
public class Title {

    @Id
    @Column
    @GeneratedValue(strategy=GenerationType.AUTO)
    private long id;
    @Column
    private String title;
    @Column
    private String url;
    @Column
    private String site;
    @Column
    private String image;
    @Column
    private Date lastUpdated;
    @Column
    private int chapter;
    @ManyToMany(mappedBy = "savedTitles",  cascade = CascadeType.PERSIST)
    Set<User> savedUsers = new HashSet<>();

    public Set<User> GetUsers(){
        return savedUsers;
    }

    public Title( String seriesTitle, String url, String site, String image, Date lastUpdated, int chapter,
            Set<User> savedUsers) {
        
        this.title = seriesTitle;
        this.url = url;
        this.site = site;
        this.image = image;
        this.lastUpdated = lastUpdated;
        this.chapter = chapter;
        this.savedUsers = savedUsers;
    }
    
    
}
