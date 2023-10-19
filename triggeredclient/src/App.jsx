import { useState } from 'react'
import './App.css'
import Header from './Header/Header'
import { BrowserRouter as Router,Routes, Route, Link } from 'react-router-dom'; 
import Home from './Homepage/Home'
import About from './About/About'
import Login from './Login/Login'
import Profile from './Profile/Profile';
import Finder from './Finder/Finder'
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  const [loggedIn, setLoggedIn] = useState(false)

  return (
    <>
      <Router> 
      <Header loggedIn={loggedIn}/>
        <Routes> 
                <Route exact path='/' element={< Home />}></Route> 
                <Route exact path='/about' element={< About />}></Route> 
                <Route exact path='/login' element={< Login setLoggedIn={setLoggedIn}/>}></Route> 
                <Route exact path='/profile' element={< Profile loggedIn={loggedIn}/>}></Route>
                <Route exact path='/search' element={< Finder loggedIn={loggedIn}/>}></Route>
                <Route exact path='/logout' element={< Home />}></Route> 
        </Routes> 

    </Router> 
    </>
  )
}

export default App
