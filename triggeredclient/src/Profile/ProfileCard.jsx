import React from 'react'
import NovelCard from '../Common/NovelCard'
import Bin from '../assets/bin.png'
import './ProfileCard.css'

function ProfileCard({novel, handleDelete}) {
  return (
    <div className='profilecard-container'>
    <NovelCard novel={novel}></NovelCard>
    <img className='h-6 w-6' src={Bin} onClick={()=> handleDelete(novel.id)}></img>
  </div>
  )
}

export default ProfileCard