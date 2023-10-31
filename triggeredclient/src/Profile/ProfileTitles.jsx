import React from 'react'
import ProfileCard from './ProfileCard'

function ProfileTitles({allNovels, handleDelete}) {
    if (allNovels.length>0){
        return (
          <div className='profile-container flex flex-col gap-2'>
            <div className='text-2xl'>Your Triggers</div>
            <div id='border'></div>
    
            {allNovels.map(x=> <ProfileCard key={x.id} novel={x} handleDelete={handleDelete}/>)
          }
          </div>
        )
      }
      return( 
        <div>You do not have any Triggers. Add some now!</div>
      )
}

export default ProfileTitles