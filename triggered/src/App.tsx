import { useState, createContext, useContext } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import NavigationBar from './Common/NavigationBar'
import LoginContext from './Common/LoginContext'

function App() {
  const [count, setCount] = useState(0)

  return (
    <LoginContext.Provider value={{isLoggedIn: false}}>
      <NavigationBar/>
    </LoginContext.Provider>
  )
}

export default App
