/* eslint-disable no-lone-blocks */
import React from 'react'
import SidebarHeader from './SidebarHeader'
import './Sidebar.css'

export default function Sidebar({ userDetails }) {
  return (
    <div className='sidenav'>
      <SidebarHeader user={userDetails} />
    </div>
  )
}
