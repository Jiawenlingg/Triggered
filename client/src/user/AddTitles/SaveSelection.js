import React from 'react'
import "./SaveSelection.css"
import { ACTIONS } from './AddTitle'
function SaveSelection(props) {
  // function HandleClick(obj){
  //   props.removeItem(obj)
  //   console.log(`removed item: ${obj.title}`)
  // }

  if(props.selection.length > 0)
  return (
    <span className='selection'>
        {props.selection.map(x=> <div className='selection-item' key={x.title}><p>{x.title}</p><p className='cancel' onClick={()=> props.dispatch({type: ACTIONS.DELETE, payload: x})}>x</p></div>)}
        <button><span>Save</span></button>
    </span> 
  )
  else return (<span></span>)
}

export default React.memo(SaveSelection)