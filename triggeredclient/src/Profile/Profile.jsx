import React, { useEffect, useState } from 'react'
import AuthService from '../Services/AuthService'
import NovelCard from '../Common/NovelCard'

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
      {allNovels.map((x,i)=> <NovelCard key={i} novel={x}></NovelCard>)}
    </div>
  )
}

export default Profile