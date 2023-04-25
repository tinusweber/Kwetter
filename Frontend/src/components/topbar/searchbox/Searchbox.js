import React, { useState } from 'react'
import SearchBar from 'material-ui-search-bar'

import './searchbox.css'

export default function Searchbox() {
  const [value, setValue] = useState('')

  const doSearch = (query) => {
    if (query && query !== '') {
      document.location.replace('/search/' + query)
    }
  }
  return (
    <div>
      <SearchBar
        className='searchbox'
        value={value}
        placeholder='Search for user'
        onChange={(newValue) => setValue(newValue)}
        onRequestSearch={(e) => doSearch(e)}
      />
    </div>
  )
}
