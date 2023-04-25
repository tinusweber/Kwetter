import React, { useState } from 'react'
import { TableCell, TableRow, Menu, MenuItem, IconButton } from '@material-ui/core'
import EditIcon from '@material-ui/icons/Edit'
import MoreHorizIcon from '@material-ui/icons/MoreHoriz'
import TicketDetailsDialog from '../TicketDetails/TicketDetailsDialog'
import './TicketRow.css'

export default function TicketRow({ ticket }, { key }) {
  const [anchorEl, setAnchorEl] = useState(null)
  const [openOrders, setOpenOrders] = React.useState(false)

  const handleClickOpenOrders = () => {
    handleClose()
    setOpenOrders(true)
  }

  const handleClickClose = () => {
    setOpenOrders(false)
  }

  const handleClick = (event) => {
    setAnchorEl(event.currentTarget)
  }

  const handleClose = () => {
    setAnchorEl(null)
  }

  return (
    <>
      <>
        <TableRow key={key}>
          <TableCell>{ticket.title}</TableCell>
          <TableCell>{ticket.body}</TableCell>
          <TableCell>{ticket.dateIssued}</TableCell>
          <TableCell id='buttonCell'>
            <IconButton aria-controls='simple-menu' aria-haspopup='true' onClick={handleClick}>
              <MoreHorizIcon />
            </IconButton>
            <Menu id='simple-menu' anchorEl={anchorEl} keepMounted open={Boolean(anchorEl)} onClose={handleClose}>
              <MenuItem onClick={() => handleClickOpenOrders()}>
                <EditIcon color='primary' className='MenuIcon' />
                {'View'}
              </MenuItem>
            </Menu>
          </TableCell>
        </TableRow>
      </>
      <TicketDetailsDialog openOrders={openOrders} ticket={ticket} handleClickClose={handleClickClose} />
    </>
  )
}
