import { useEffect, useState } from 'react'
import './App.css'
import Header from './Header/Header'
import { BrowserRouter as Router,Routes, Route, Link } from 'react-router-dom'; 
import Home from './Homepage/Home'
import About from './About/About'
import Login from './Login/Login'
import Profile from './Profile/Profile';
import Finder from './Finder/Finder'
import 'bootstrap/dist/css/bootstrap.min.css';
import Signup from './Signup/Signup';
import LogoutAlert from './Logout/LogoutAlert';
import { userStore } from './Store/AuthStore';
import Alert from 'react-bootstrap/Alert';
import ProtectedRoute from './Services/ProtectedRoute';

function App() {
  const [token, setToken] = useState('')
  const handleLogout = ()=> {
    const removeUser = userStore(state=> state.removeUser)
    alert("You have been logged out!")
    setToken('')
    removeUser()
    localStorage.removeItem('token')
  }
  return (
    <>

      <Router> 
      <Header handleLogout={handleLogout}/>
      <div className='root-container'>
        <Routes> 
                  <Route exact path='/' element={< Home />}></Route> 
                  <Route exact path='/about' element={< About />}></Route> 
                  <Route exact path='/login' element={< Login setToken={setToken}/>}></Route> 
                  <Route exact path='/signup' element={< Signup setToken={setToken}/>}></Route> 
                  <Route exact path='/profile' element={<ProtectedRoute token={token}>< Profile/></ProtectedRoute>}></Route>
                  <Route exact path='/search' element={<ProtectedRoute token={token}>< Finder/></ProtectedRoute>}></Route>
          </Routes> 
      </div>
    </Router> 
    </>
  )
}

export default App
