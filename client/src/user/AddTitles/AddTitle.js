import React from 'react'
import { FetchTitle } from '../../util/APIUtils'
import "./AddTitle.css"
import Result from './Result'
import SaveSelection from './SaveSelection'

export const ACTIONS = {
    ADD: 'add',
    DELETE:'delete'
}

function AddTitle() {
    const [searchResults, setSearchResults] = React.useState([])
    const [errorPresent, setErrorPresent] = React.useState(false)
    const [loading, setLoading] = React.useState(false);
    const [selection, setSelection] = React.useState([])
    const [resultCount, setResultCount] = React.useState([])

    const [formData, setFormData] = React.useState({
        searchTitle:"",
        searchSite:""
    })

    function reducer(state, action){
        switch (action.type){
            case ACTIONS.ADD:
                setSelection(prevSelection=> [...prevSelection, action.payload]);
                break; 
            case ACTIONS.DELETE:
                setSelection(prevSelection=> prevSelection.filter(item=> item.title!== action.payload.title && item.site!==action.payload.site))
                break;
            default:
                throw new Error()

        }

    }

    const[selections, dispatch] = React.useReducer(reducer, [])

    // const AddItem= (obj)=> {
    //     setSelection(prevSelection=> [...prevSelection, obj])
    // }

    // const RemoveItem= (obj)=> {
    //     setSelection(prevSelection=> prevSelection.filter(item=> item.title!== obj.title && item.site!==obj.site))
    // }

    const inputTitle = React.useRef("")
    const inputSite = React.useRef("")


function HandleSubmit(event){
    event.preventDefault()
    setLoading(true)
    setFormData({searchTitle: inputTitle.current.value, searchSite: inputSite.current.value})
    const searchResults = FetchTitle(formData)
    searchResults.then(res=>{
        setLoading(false)
        if(res.length > 0){
             setSearchResults(res)
        }
        else setErrorPresent(true)
     }).catch(error=> setErrorPresent(true))
    }



// React.useEffect( ()=>{
//         const searchResults = FetchTitle(formData)
//     searchResults.then(res=>{
//         setLoading(false)
//         if(res.length > 0){
//              setSearchResults(res)
//         }
//         else setErrorPresent(true)
//      }).catch(error=> setErrorPresent(true))
//     }
//     ,[formData])

  return (
    <>
    {loading && <div className='background-blur'><div className='loader'></div></div>}
    <div className='container'>
        <form onSubmit={HandleSubmit}>
            <input type="text" ref={inputTitle} className="searchTitle" placeholder='Solo Levelling'/>
            <select
            ref={inputSite}
            id="searchSite"
            >
                <option value="asura">Asura</option>
                <option value="novelupdates">Novel Updates</option>
                <option value="mangago">Mangago</option>
            </select>
            <button ><span>Search</span></button>
        </form>
        <div className='main-section'>
        <span className='results'>
            {errorPresent? <h2>No results found!</h2> : searchResults? searchResults.map(res=> <Result key={res.url} result={res} dispatch={dispatch}/>) : <h2>Hit the search button now!</h2>}
        </span>

        <span className='selection-section'>
            {searchResults && <SaveSelection selection={selection} dispatch={dispatch}/>}
        </span>
    </div>
    </div>
    </>
  )
}

export default AddTitle