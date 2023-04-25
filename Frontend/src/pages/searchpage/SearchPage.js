import React from 'react'
import SearchResultList from '../../components/search/searchResultList/SearchResultList'
import { useHistory } from 'react-router-dom'
import './Searchpage.css'
export default function SearchPage() {
  let history = useHistory()

  let query = history.location.pathname.replace('/search/', '')

  return (
    <div className='root'>
      <div className='results'>
        <SearchResultList query={query} />
      </div>
    </div>
  )
}
