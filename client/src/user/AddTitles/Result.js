import React from 'react'
import './Result.css'
import { ACTIONS } from './AddTitle'

function Result(props) {
  console.log(props)
  function HandleOnClick(){
    console.log(`${props.result} selected!`)
    props.dispatch({type: ACTIONS.ADD, payload: props.result})
  }

  const [selected, setSelected] = React.useState(false)
  return (
      <span className='result-card'>
        <div className='image-section' onClick={HandleOnClick}>
          <img className='image' src={props.result.image} alt={props.result.title} loading='lazy'/>
        </div>
        <div className='result-title'>
          <p><a href={props.result.url} target="_blank" rel="noopener noreferrer">{props.result.title}</a></p>
        </div>
      </span>


  )
}

export default Result