import React, { useEffect, useState } from 'react'
import AuthService from '../Services/AuthService'
import NovelCard from '../Common/NovelCard'
import ProfileCard from './ProfileCard'

function Profile() {
  console.log("profile")
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


  return (
    <div className='profile-container flex flex-col gap-5'>
      {allNovels.map(x=> {
        console.log(x.title)
        return (<ProfileCard key={x.id} novel={x} handleDelete={handleDelete}/>)
    })}
    </div>
  )
}

export default Profile