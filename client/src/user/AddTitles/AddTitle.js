import React from 'react'
import { searchTitle } from '../../util/APIUtils'
import "./AddTitle.css"
import Result from './Result'

function AddTitle() {
    const [searchResults, setSearchResults] = React.useState([])
    const [errorPresent, setErrorPresent] = React.useState(false)
    const [loading, setLoading] = React.useState(false);

    const [formData, setFormData] = React.useState({
        searchTitle:"",
        searchSite:""
    })

function HandleSubmit(event){
    setErrorPresent(false)
    console.log(`Form submitted!: ${formData.searchSite}`)
    event.preventDefault()
    setLoading(true)
    
    const searchResults = searchTitle(formData)
    searchResults.then(res=>{
        setLoading(false)
        setSearchResults(res) })
    console.log(searchResults)
}

function HandleChange(event){
    console.log(event.target.id, event.target.value)
    setFormData({
        ...formData, 
        [event.target.id]: event.target.value
    })
}
  return (
    <>
        <form onSubmit={HandleSubmit}>
            <input type="text" value={formData.searchTitle} onChange={HandleChange} id="searchTitle"/>
            <select
            onChange={HandleChange}
            value={formData.searchSite}
            id="searchSite"
            >
                <option value="asura">Asura</option>
                <option value="novelupdates">Novel Updates</option>
                <option value="mangago">Mangago</option>
            </select>
            <button>Search</button>
        </form>

        <span className='results'>
            {/* <h2 style={{display: errorPresent? 'block': 'none'}}>No results found!</h2> */}
            {searchResults? searchResults.map(res=> <Result result={res}/>) : <h2>Hit the search button now!</h2>}
        </span>
    </>
  )
}

export default AddTitle