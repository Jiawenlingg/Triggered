package com.spring.login.service;

import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;

public interface UserService {
    UserDetails loadUserByEmail(String email) throws UsernameNotFoundException;

    UserDetails loadUserById(long id);
}
