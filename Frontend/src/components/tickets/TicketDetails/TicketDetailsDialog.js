import React from 'react'
import { makeStyles, Dialog, AppBar, Toolbar, Typography, IconButton } from '@material-ui/core'
import './TicketDetailsDialog.css'
import CloseIcon from '@material-ui/icons/Close'
import TicketResponse from '../TicketDetails/TicketResponse'
import TicketReplyBox from '../ticketReplyBox/TicketReplyBox'

const useStyles = makeStyles((theme) => ({
  appBar: {
    position: 'relative'
  },
  title: {
    marginLeft: theme.spacing(2),
    flex: 1
  }
}))

export default function TicketDetailsDialog({ openOrders, ticket, handleClickClose }) {
  const classes = useStyles()
  const InitialMessage = {
    replyBy: ticket.issuedBy,
    body: ticket.body
  }

  return (
    <Dialog
      fullScreen
      open={openOrders}
      keepMounted
      onClose={handleClickClose}
      aria-labelledby='alert-dialog-slide-title-orders'
      aria-describedby='alert-dialog-slide-description-orders'
    >
      <AppBar className={classes.appBar} color='primary'>
        <Toolbar>
          <Typography variant='h6' className={classes.title}>
            {'Ticket id'}: {ticket.idString}, {'started'}: {ticket.dateIssued}
          </Typography>
          <IconButton edge='end' color='inherit' onClick={handleClickClose} aria-label='close'>
            <CloseIcon />
          </IconButton>
        </Toolbar>
      </AppBar>
      <div className='replies'>
        <TicketResponse reply={InitialMessage} />
        {ticket.replies?.map((reply, index) => (
          <TicketResponse reply={reply} />
        ))}
      </div>
      {ticket.status !== 'DONE' ? <TicketReplyBox ticketId={ticket.idString} /> : null}
    </Dialog>
  )
}
