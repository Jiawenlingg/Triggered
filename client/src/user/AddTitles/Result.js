import React from 'react'
import './Result.css'

function Result(props) {
    console.log(props.result)
  return (
      <span className='result-card'>
        <img src={props.result.image} alt={props.result.title}/>
        <div className="middle">
          <button >Add</button>
        </div>
        <p><a href={props.result.url} target="_blank" rel="noopener noreferrer">{props.result.title}</a></p>
      </span>


  )
}

export default Result