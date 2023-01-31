package com.spring.login.controller;

import java.util.List;
import java.util.Optional;

import com.spring.login.model.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.spring.login.exception.ResourceNotFoundException;
import com.spring.login.model.Title;
import com.spring.login.repository.TitleRepository;
import com.spring.login.repository.UserRepository;
import com.spring.login.security.CurrentUser;
import com.spring.login.security.UserPrincipal;
import com.spring.login.service.AsuraService;

@RestController
public class TitleController {
    @Autowired
    TitleRepository titleRepository;

    @Autowired
    UserRepository userRepository;

    @Autowired
    AsuraService asuraService;

    @GetMapping("/getAll")
    public ResponseEntity<List<Title>> GetAllTitles(@CurrentUser UserPrincipal userPrincipal){
        
        Long id =  userPrincipal.getId();
        List<Title> titles = titleRepository.findTitleById(id);
        if(titles==null || titles.isEmpty()) throw new ResourceNotFoundException("No titles found for this user.");
        return new ResponseEntity<>(titles, HttpStatus.OK);
    }

    @GetMapping("/search")
    public ResponseEntity<?> SearchTitle(@CurrentUser UserPrincipal userPrincipal, @RequestParam String title, @RequestParam String site){
        Optional<List<Title>> res= asuraService.searchSeries(title);
        if (res.isPresent()) return new ResponseEntity<>(res.get(), HttpStatus.OK);
        else return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }

    @PostMapping("/saveTitles")
    public ResponseEntity<String> SaveTitles(@CurrentUser UserPrincipal userPrincipal,@RequestBody List<Title> titles){
        long id =  userPrincipal.getId();
        Optional<User> user = userRepository.findById(id);

        try{
            user.map(u-> {
                titles.forEach(x-> {
                    Optional<Title> opt = titleRepository.findByUrl(x.getUrl());
                    if(opt.isPresent()) u.AddTitle(opt.get());
                    else {
                        titleRepository.save(x);
                        u.AddTitle(x);
                    }
                });
                return userRepository.save(u);
            }).orElseThrow(()->new ResourceNotFoundException("Unable to load user."));

            return new ResponseEntity<String>("Successfully saved", HttpStatus.ACCEPTED);
        }
        catch (Exception e)
        {
            return new ResponseEntity<String>("Failed to save", HttpStatus.BAD_REQUEST);
        }
    }

    @DeleteMapping("/deleteTitles")
    public ResponseEntity<String> DeleteTitles(@CurrentUser UserPrincipal userPrincipal,@RequestBody List<Title> titles){
        long id =  userPrincipal.getId();
        Optional<User> user = userRepository.findById(id);

        try{
            user.map(u-> {
                titles.forEach(x-> {
                    Optional<Title> opt = titleRepository.findByUrl(x.getUrl());
                    opt.ifPresent((value)-> {
                        u.RemoveTitle(value);
                    });

                });
                return userRepository.save(u);
            }).orElseThrow(()->new ResourceNotFoundException("Unable to load user."));
            return new ResponseEntity<String>("Successfully deleted", HttpStatus.ACCEPTED);
        } catch (Exception e) {
            return new ResponseEntity<String>("Failed to delete", HttpStatus.BAD_REQUEST);
            
        }
    }
}
