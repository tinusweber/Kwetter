import React, { useState } from 'react'
import { Card, CardContent, Button, TextField } from '@material-ui/core'
import ticketService from '../../../services/ticket.service'
import './TicketReplyBox.css'

export default function WriteReplyBox({ ticketId }) {
  const [postText, setPostText] = useState('')

  const postReply = () => {
    console.log(ticketId, postText)
    ticketService.replyToTicket(ticketId, postText).then(document.location.reload())
  }
  return (
    <Card className='replybox'>
      <CardContent>
        <div className='cardItems'>
          <TextField
            className='textInput'
            id='filled-multiline-static'
            multiline
            variant='outlined'
            placeholder={'Enter your reply'}
            onChange={(e) => setPostText(e.target.value)}
          />
          <Button className='sendButton' variant='contained' color='primary' onClick={() => postReply()}>
            {'Add reply'}
          </Button>
        </div>
      </CardContent>
    </Card>
  )
}
