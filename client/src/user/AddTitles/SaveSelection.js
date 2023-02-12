import React from 'react'
import "./SaveSelection.css"
import { ACTIONS } from './AddTitle'
function SaveSelection(props) {

  const [width, setWidth] = React.useState(window.innerWidth);
  const breakpoint = 700;
  React.useEffect(() => {
   const handleResizeWindow = () => setWidth(window.innerWidth);
    // subscribe to window resize event "onComponentDidMount"
    window.addEventListener("resize", handleResizeWindow);
    return () => {
      // unsubscribe "onComponentDestroy"
      window.removeEventListener("resize", handleResizeWindow);
    };
  }, []);

  if(props.selection.length > 0 && width>breakpoint)
  return (
    <span className='selection'>
        {props.selection.map((x, index)=> <div className='selection-item' key={index}><p>{x.title}</p><p className='cancel' onClick={()=> props.dispatch({type: ACTIONS.DELETE, payload: x})}>x</p></div>)}
        <button className='save-button'>Save {props.selection.length} items</button>
    </span> 
  )
  if(props.selection.length > 0 && width<=breakpoint) return (
    <div>
      Save {props.selection.length} items
    </div>
  )
  else return (<span></span>)
}

export default React.memo(SaveSelection)