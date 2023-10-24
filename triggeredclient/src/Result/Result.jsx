import React from 'react'
import './Result.css'

function Result({result, handleAddTrigger}) {
  return (
    <div className='result-container'>
      <div className='text-xl mb-1'>Results: </div>
      <div className='result-details'>
      <img src={result.Image}></img>
      <div className='result-text ml-6'>
        <a href={result.Url} target="_blank" rel="noopener noreferrer" className='cursor-pointer text-xl'>{result.Title}</a>
        <p>Site: {result.Website}</p>
        <p>Last updated: {result.LastUpdate}</p>
        <p>Latest chapter: {result.LatestChapter}</p>
      <button className='mt-4' onClick={handleAddTrigger}>ADD TRIGGER</button>
      </div>
      </div>
    </div>
  )
}

export default Result