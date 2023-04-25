import React from 'react'
import Comment from '../comment/Comment'
export default function Commentlist({ comments }) {
  return comments ? (
    <div>
      {comments.map((comment, idx) => (
        <Comment key={idx} postedBy={comment.commenterId} text={comment.body}></Comment>
      ))}
    </div>
  ) : null
}
