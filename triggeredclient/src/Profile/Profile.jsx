import React, { useEffect, useState } from 'react'
import AuthService from '../Services/AuthService'
import NovelCard from '../Common/NovelCard'
import ProfileTitles from './ProfileTitles'
import TelegramProfile from './TelegramProfile'

function Profile() {
  const [allNovels, setAllNovels] = useState([])

  const handleDelete = (ID)=> {
    console.log(ID)
    AuthService.delete('/novel/delete/' + ID)
    .then(res => setAllNovels(prev=> prev.filter(x=> x.id!==ID)))
    .catch(err=> alert("An error occured while trying to remove Trigger"))
  }

  useEffect(()=>
  {
    AuthService.get('/novel/all')
    .then(res=> setAllNovels(res.data))
    .catch(err=> alert("An error occured while loading your profile!"))
  },[])

  return( 
    <>
    <TelegramProfile></TelegramProfile>
    <ProfileTitles allNovels={allNovels} handleDelete={handleDelete}></ProfileTitles>
    </>
  )
}

export default Profile