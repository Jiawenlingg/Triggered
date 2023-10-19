import React, { useState } from "react";
import "./Header.css";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import Menu from "./../assets/menu.png";

function Header({ loggedIn }) {
  const [openHamburger, setOpenHamburger] = useState(false);
  const handleToggle = () => {
    setOpenHamburger((prev) => !prev);
  };

  return (
    <>
      <div className="header">
        <div href="#default">CompanyLogo</div>
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
            <a>
              <Link onClick={handleToggle} to="/">Home</Link>
            </a>
          </li>
          <li>
            <a>
              <Link onClick={handleToggle} to="/about">About</Link>
            </a>
          </li>
          <li>
            <a>
              <Link onClick={handleToggle} to="/login">Login</Link>
            </a>
          </li>
          {loggedIn && (
            <li>
                      <a>
                        <Link onClick={handleToggle} to="/profile">Profile</Link>
                      </a>
                    </li>
            )}
               {loggedIn && (
                 <li>
                      <a>
                        <Link onClick={handleToggle} to="/Logout">Logout</Link>
                      </a>
                    </li>
            )}
            </ul>
          </nav>
      </div>
      <hr class="solid"></hr>
    </>
  );
}

export default Header;
