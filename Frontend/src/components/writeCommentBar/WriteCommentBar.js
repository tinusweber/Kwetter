import React, { useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Card from '@material-ui/core/Card'
import CardContent from '@material-ui/core/CardContent'
import Button from '@material-ui/core/Button'
import TextField from '@material-ui/core/TextField'
import './WriteCommentBar.css'
import SendIcon from '@material-ui/icons/Send'
import commentService from '../../services/comment.service'
const useStyles = makeStyles({
  root: {
    width: 480,
    borderRadius: 10
  }
})

export default function WriteCommentBar({ postId }) {
  const classes = useStyles()
  const [postText, setPostText] = useState('')

  const postComment = () => {
    commentService.AddComment(postId, postText).then(() => {
      setPostText('')
    })
  }
  return (
    <Card className={classes.root}>
      <CardContent>
        <div className='cardItems'>
          <TextField
            className='textInput'
            id='filled-multiline-static'
            multiline
            variant='outlined'
            placeholder='This is great'
            onChange={(e) => setPostText(e.target.value)}
          />
          <Button className='sendButton' variant='contained' color='primary' onClick={postComment} endIcon={<SendIcon />}>
            Comment
          </Button>
        </div>
      </CardContent>
    </Card>
  )
}
