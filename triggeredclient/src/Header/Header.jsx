import React, { useState } from "react";
import "./Header.css";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import Menu from "./../assets/menu.png";
import { userStore } from "../Store/AuthStore";
import { useNavigate } from "react-router-dom";

function Header({ handleLogout}) {
  const [openHamburger, setOpenHamburger] = useState(false);
  const navigate = useNavigate()
  const handleToggle = () => {
    setOpenHamburger((prev) => !prev);
  };
  const user = userStore(state=> state.user)
  const loggedIn = user!=''
  

  return (
    <>
      <div className="header">
        <div href="#default"><Link to="/">Logo</Link></div>
        <div className="hamburger">
          <img
            className="cursor-pointer nav-btn"
            src={Menu}
            alt="logo"
            onClick={handleToggle}
          ></img>
        </div>
        <nav className={openHamburger? "navigation-menu expanded" : "navigation-menu"}>
          <ul>
  
          <li>
              <Link onClick={handleToggle} to="/about"><span>ABOUT</span></Link>
          </li>
          {!loggedIn && <li>
              <Link onClick={handleToggle} to="/signup"><span>SIGN UP</span></Link>
          </li>}
          {!loggedIn && <li>
              <Link onClick={handleToggle} to="/login"><span>LOGIN</span></Link>
          </li>}
          {loggedIn && (
            <li>
              <Link onClick={handleToggle} to="/profile"><span>PROFILE</span></Link>
             </li>
            )}
          {loggedIn && <li>
            <Link onClick={handleToggle} to="/search"><span>ADD TRIGGERS</span></Link>
            </li>}
          {loggedIn && (
            <li>
              <Link onClick={()=> {handleLogout(); navigate('/')}} to="/"><span>LOGOUT</span></Link>
              </li>
            )}
            </ul>
          </nav>
      </div>
      <hr className="solid"></hr>
    </>
  );
}

export default Header;
