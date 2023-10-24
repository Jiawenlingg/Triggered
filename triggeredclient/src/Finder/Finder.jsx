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
  const [searchUrl, setSearchUrl] = useState('')
  // const token = AuthServices.getAuthUser()
  const [inputError, setInputError] = useState('')
  const [results, setResults] = useState({Title: '', Website: '', Url: '', Image: '', LastUpdate: '', LatestChapter:''})

  const handleInput = (event)=> {
    console.log(event.target.value)
    setSearchUrl(event.target.value)
  }

  const handleSubmit = ()=> {
    event.preventDefault()
    AuthServices.get('/novel', { params: {search: searchUrl}} )
    .then(res=> {
      console.log(res.data)
      const result = res.data
      setResults({Title: result.title, Website:result.website, Url:result.url, Image: result.image, LastUpdate: result.lastUpdate, LatestChapter: result.latestChapter })
    })
    .catch(err=> {
      console.log(err.response)
      setInputError(err.response.data)
    })
  }

  const handleAddTrigger = ()=> {
    AuthServices.post('/novel/save', results)
    .then(res=> {
      setResults({Title: '', Website: '', Url: '', Image: '', LastUpdate: '', LatestChapter:''})
      toast.success("Successfully added new Trigger!",  {
        position: "bottom-center"} )
    })
    .catch(err=> {
      alert("An error occurred while trying to save Triggers!")
    })
  }

  return (
    <div className='finder-container'>
      <div className='text-4xl'>ADD TRIGGERS </div>
      <div className="finder-border"></div>
      <form onSubmit={handleSubmit} className='finder-form'>
        <input className='search-input' placeholder='Enter the Url of the series' onChange={handleInput}></input>
        <Button className='finder-button' type='submit' variant="outline-danger">Search</Button>{' '}
      </form>
        <div className='text-xs' style={{ color: 'red' }}>{inputError}</div>
      {results.Url && <Result result={results} handleAddTrigger={handleAddTrigger}></Result>}
      <Toaster></Toaster>
    </div>
  
  )
}

export default Finder