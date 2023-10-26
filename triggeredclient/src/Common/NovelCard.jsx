import React from 'react'
import './NovelCard.css'

function NovelCard({novel}) {
  return (
    <div className='result-details p-4'>
        <div className='result-img'>
            <img src={novel.image}></img>
        </div>
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