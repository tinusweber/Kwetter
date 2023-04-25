import React from 'react'
import WritepostBar from '../../components/writepostbar/WritepostBar'
import Postlist from '../../components/post/postlist/Postlist'
import './Mainpage.css'
function Mainpage() {
  return (
    <div>
      <div className='posts'>
        <WritepostBar />

        <Postlist />
      </div>
    </div>
  )
}

export default Mainpage
