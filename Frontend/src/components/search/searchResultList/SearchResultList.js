import React, { useState, useEffect } from 'react'
import List from '@material-ui/core/List'
import Divider from '@material-ui/core/Divider'
import Searchresult from '../searchresult/Searchresult'

import Paper from '@material-ui/core/Paper'

import './SearchResultList.css'
import userService from '../../../services/user.service'
import { Typography } from '@material-ui/core'

export default function SearchResultList({ query }) {
  const [results, setResults] = useState([])
  useEffect(() => {
    userService.getAll().then((response) => {
      console.log(response)
      var filtered = response.filter((user) => user.displayName.includes(query))
      setResults(filtered)
    })
  }, [query])
  return results.length !== 0 ? (
    <Paper className='listContainer'>
      <Typography>Results matching query "{query}"</Typography>
      <List className='list'>
        {results.map((result, idx) => (
          <div id={idx}>
            <Searchresult result={result} id={idx} data-testid='searchResult' />
            <Divider variant='inset' component='li' id={idx} />
          </div>
        ))}
      </List>
    </Paper>
  ) : (
    <div className='listContainer'>
      <h1>No results found</h1>
    </div>
  )
}
