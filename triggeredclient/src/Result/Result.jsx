import React from 'react'
import './Result.css'
import NovelCard from '../Common/NovelCard'

function Result({result, handleAddTrigger}) {
  console.log(result)
  return (
    <div className='result-container'>
      <div className='text-xl mb-4'>Results: </div>
      <NovelCard novel={result}></NovelCard>
      <button className='mt-4' onClick={handleAddTrigger}>ADD TRIGGER</button>
    </div>
  )
}

export default Result