import React, { useEffect, useState } from 'react'
import AuthService from '../Services/AuthService'

function Profile() {
  const [allNovels, setAllNovels] = useState([])
  useEffect(()=>
  {
    AuthService.get('/novel/all')
    .then(res=> setAllNovels(res.data))
    .catch(err=> alert("An error occured while loading your profile!"))
  },[])
  return (
    <div className='profile-container'>

      {allNovels.map(x=> <div>{x.title}</div>)}
    </div>
  )
}

export default Profile