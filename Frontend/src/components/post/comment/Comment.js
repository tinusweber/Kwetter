import React from 'react'
import { makeStyles } from '@material-ui/core/styles'
import { ListItem, ListItemText, ListItemAvatar, Avatar, Typography } from '@material-ui/core'
import './Comment.css'
const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
    backgroundColor: theme.palette.background.paper
  },
  fonts: {
    fontWeight: 'bold'
  },
  inline: {
    display: 'inline'
  }
}))

const Comment = ({ key, postedBy, text }) => {
  const classes = useStyles()
  return (
    <div id='comment'>
      <ListItem key={key} alignItems='flex-start'>
        <ListItemAvatar>
          <Avatar alt='avatar' src={postedBy.profilePicture} />
        </ListItemAvatar>
        <div className='commentBody'>
          <ListItemText primary={<Typography className={classes.fonts}>{postedBy.displayName}</Typography>} secondary={<>{text}</>} />
        </div>
      </ListItem>
    </div>
  )
}
export default Comment
