package com.spring.login.repository;

import java.util.List;
import java.util.Optional;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import com.spring.login.model.Title;

@Repository
public interface TitleRepository extends CrudRepository<Title, String> {
    List<Title> findTitleById(Long id);
    Optional<Title> findByUrl(String url);
}

