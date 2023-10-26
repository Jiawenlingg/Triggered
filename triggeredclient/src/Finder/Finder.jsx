import React, { useState } from 'react'
import Card from 'react-bootstrap/Card';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import './Finder.css'
import { Button } from 'react-bootstrap';
import Result from '../Result/Result';
import axios from 'axios';
import AuthServices from '../Services/AuthService'
import toast, { Toaster } from "react-hot-toast";

function Finder() {
  const [searchurl, setSearchurl] = useState('')
  // const token = AuthServices.getAuthUser()
  const [inputError, setInputError] = useState('')
  const [results, setResults] = useState({title: '', website: '', url: '', image: '', lastUpdate: '', latestChapter:''})

  const handleInput = (event)=> {
    console.log(event.target.value)
    setSearchurl(event.target.value)
  }

  const handleSubmit = ()=> {
    event.preventDefault()
    setInputError('')
    AuthServices.get('/novel', { params: {search: searchurl}} )
    .then(res=> {
      const result = res.data
      setResults({title: result.title, website:result.website, url:result.url, image: result.image, lastUpdate: result.lastUpdate, latestChapter: result.latestChapter })
    })
    .catch(err=> {
      console.log(err.response)
      setInputError(err.response.data)
    })
  }

  const handleAddTrigger = ()=> {
    AuthServices.post('/novel/save', results)
    .then(res=> {
      setSearchurl('')
      setResults({title: '', website: '', url: '', image: '', lastUpdate: '', latestChapter:''})
      toast.success("Successfully added new Trigger!",  {
        position: "bottom-center"} )
    })
    .catch(err=> {
      toast.error("An error occurred while trying to save Trigger!")
    })
  }

  return (
    <div className='finder-container'>
      <div className='text-4xl'>ADD TRIGGERS </div>
      <div id="border"></div>
      <form onSubmit={handleSubmit} className='finder-form'>
        <input className='search-input' placeholder='Enter the url of the series' value={searchurl} onChange={handleInput}></input>
        <Button className='finder-button' type='submit' variant="outline-danger">Search</Button>{' '}
      </form>
        <div className='text-xs self-center' style={{ color: 'red' }}>{inputError}</div>
      {results.url && <Result result={results} handleAddTrigger={handleAddTrigger}></Result>}
      <Toaster></Toaster>
    </div>
  
  )
}

export default Finder