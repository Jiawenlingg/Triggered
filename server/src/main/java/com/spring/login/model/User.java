package com.spring.login.model;

import com.fasterxml.jackson.annotation.JsonIgnore;
import lombok.Data;

import java.util.HashSet;
import java.util.List;
import java.util.Set;

import javax.persistence.*;

@Data
@Entity
@Table
public class User {

    @Id
    @Column
    @GeneratedValue(strategy=GenerationType.AUTO)
    private long id;

    @Column
    private String name;
    @Column
    private String email;
    @Column
    private String imageUrl;
    @Column
    private Boolean emailVerified = false;

    @JsonIgnore
    private String password = null;

    private AuthProvider provider;

    private String providerId;

    @ManyToMany(fetch = FetchType.LAZY, cascade = CascadeType.PERSIST)
    @JoinTable(
    name = "user_title", 
    joinColumns = @JoinColumn(name = "user_id"), 
    inverseJoinColumns = @JoinColumn(name = "title_id"))
    Set<Title> savedTitles = new HashSet<>();

    public Set<Title> GetTitles(){
        return savedTitles;
    }

    public void AddTitles(List<Title> titles) {
        titles.forEach(x-> {
            if(x!=null){
                x.GetUsers().add(this);
                this.savedTitles.add(x);
            }
        });
    }

    public void AddTitle(Title title){
        this.savedTitles.add((title));
        title.getSavedUsers().add(this);
    }

    public void RemoveTitle(Title title){
        this.savedTitles.remove((title));
        title.getSavedUsers().remove(this);
    }
      
    public void RemoveTitles(List<Title> titles) {
    titles.forEach(x-> {
        if(x!=null){
            savedTitles.remove(x);
            x.GetUsers().remove(this);
        }
    });
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }
}

