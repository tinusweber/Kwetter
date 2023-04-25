import React from 'react'
import './TicketResponse.css'
export default function TicketResponse({ reply }) {
  return reply.replyBy.role === 'Customer' ? (
    <div className='container'>
      <img src={reply.replyBy.profilePicture} alt='Avatar' />
      <p>{reply.body}</p>
      <span className='time-right'>{reply.dateReplied}</span>
    </div>
  ) : (
    <div className='container darker'>
      <p>{reply.replyBy.email}</p>
      <img src={reply.replyBy.profilePicture} alt='Avatar' className='right' />
      <p>{reply.body}</p>
      <span className='time-left'>{reply.dateReplied}</span>
    </div>
  )
}
