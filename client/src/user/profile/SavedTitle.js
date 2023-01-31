import React from 'react'
import { Link } from 'react-router-dom'

function SavedTitle(props) {
  return (
    <>
      <li className='cards__item'>
        <Link className='cards__item__link' to={props.url}>
          <figure className='cards__item__pic-wrap'>
            <img
              src={props.image}
            />
          </figure>
          <div className='cards__item__info'>
            <h5 className='cards__item__text'>{props.title}</h5>
            <h5 className='cards__item__text'>{props.lastUpdated}</h5>
          </div>
        </Link>
      </li>
    </>
  )
}

export default SavedTitle