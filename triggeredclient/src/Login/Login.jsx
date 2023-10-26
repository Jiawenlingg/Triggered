import React, { useState } from 'react'
import { Link } from 'react-router-dom'
import './Login.css'
import '../Form.css'
import Button from 'react-bootstrap/Button';
import User from './../assets/user.png'
import Lock from './../assets/lock.png'
import axios from 'axios';
import { BASEURL } from '../Constants';
import { useNavigate } from 'react-router-dom';
import { userStore } from '../Store/AuthStore';



function Login({setToken}) {
    const navigate = useNavigate()
    const setUser = userStore((state)=> state.setUser)

    const [formdata, setFormdata] = useState({username:'', password: ''})
    const [inputError, setInputError] = useState('')
    const handleChange = (event) => {
        const {name, value} = event.target
        setFormdata(prev=> ({...prev, [name]: value}))
    }

    function validate() {
        return username.length > 0 && password.length > 0;
    }


    const handleSubmit = (event) => {
        event.preventDefault();
        setInputError('')
        if(formdata.username.length<=0 || formdata.password.length <=0) setInputError("One or more fields missing")
        axios.post(BASEURL+"api/Authentication/login", formdata)
        .then((response) => {
            localStorage.setItem("token", JSON.stringify(response.data));
            setUser(formdata.username)
            setToken(response.data)
            navigate("/profile")
        })
        .catch(function (error) {
            console.log(error);
            setInputError("Invalid username or password")
        });

    }


  return (
    <div className='container'>
        <h1 className='text-4xl'>Login</h1>
        <div id='border'></div>
        <form className='login-form' onSubmit={handleSubmit}>
            <div className='input-form'>
                <img src={User}></img>
                <input placeholder='Username' name="username" value={formdata.username} onChange={handleChange}></input>
            </div> 
            <div className='input-form'>
                <img src={Lock}></img>
                <input placeholder='Password' name="password" type='password' value={formdata.password} onChange={handleChange}></input>
            </div>
            <div className='h-10'>
                <div style={{ color: 'red' }}>{inputError}</div >
            </div>
            <Button size='lg' variant='danger' type='submit'>LET'S GO</Button>
        </form>
        <a className='links'>Forgot password?</a>
        <div>Don't have an account? Sign up for one <Link className='links'>here</Link></div>
    </div>
  )
}

export default Login