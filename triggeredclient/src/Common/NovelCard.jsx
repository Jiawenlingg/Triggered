import React from 'react'
import './NovelCard.css'
import ErrorImg from '../assets/Error.png'

function NovelCard({novel}) {
  return (
    <div className='result-details'>
    <img className='result-img' src={novel.image}
    onError={(e) => {e.target.src = ErrorImg}}></img>

      <div className='result-text ml-6'>
        <a href={novel.url} target="_blank" rel="noopener noreferrer" className='cursor-pointer text-l'>{novel.title}</a>
        <p>Site: {novel.website}</p>
        <p>Last updated: {new Date(novel.lastUpdate).toLocaleDateString()}</p>
        <p>Latest chapter: {novel.latestChapter}</p>
      </div>
      </div>
  )
}

export default NovelCard