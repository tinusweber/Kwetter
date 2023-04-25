import React, { useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import clsx from 'clsx'
import Card from '@material-ui/core/Card'
import CardHeader from '@material-ui/core/CardHeader'
import CardMedia from '@material-ui/core/CardMedia'
import CardContent from '@material-ui/core/CardContent'
import CardActions from '@material-ui/core/CardActions'
import IconButton from '@material-ui/core/IconButton'
import Typography from '@material-ui/core/Typography'
import { red } from '@material-ui/core/colors'
import ExpandMoreIcon from '@material-ui/icons/ExpandMore'
import WriteCommentBar from '../../writeCommentBar/WriteCommentBar'
import { Collapse, Menu, MenuItem, Button } from '@material-ui/core'
import Commentlist from '../commentlist/Commentlist'
import './Post.css'
import ReportDialog from '../reportDialog/ReportDialog'
import { useHistory } from 'react-router-dom'
import userService from '../../../services/user.service'
import commentService from '../../../services/comment.service'

const useStyles = makeStyles((theme) => ({
  root: {
    width: 480,
    marginBottom: '40px',
    borderRadius: 10
  },
  media: {
    height: 0,
    paddingTop: '56.25%' // 16:9
  },
  expand: {
    transform: 'rotate(0deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
      duration: theme.transitions.duration.shortest
    })
  },
  expandOpen: {
    transform: 'rotate(180deg)'
  },
  avatar: {
    backgroundColor: red[500]
  }
}))

export default function Post({ imgData, subtitle, postedBy, postId }) {
  const history = useHistory()
  const classes = useStyles()
  const [expanded, setExpanded] = React.useState(false)
  const [anchorEl, setAnchorEl] = React.useState(null)
  const [poster, setPoster] = React.useState({})
  const [comments, setComments] = React.useState([])

  const [showReportDialog, setShowReportDialog] = useState(false)
  const handleExpandClick = () => {
    setExpanded(!expanded)
  }
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget)
  }

  const handleClose = () => {
    setAnchorEl(null)
  }

  useEffect(() => {
    userService.getById(postedBy).then((user) => setPoster(user))
    commentService.GetByPost(postId).then((cmt) => setComments(cmt))
  }, [postedBy, postId])

  return (
    <>
      <Card className={classes.root}>
        <div className='postHeader'>
          <CardHeader
            onClick={() => history.push('/user/' + poster.ownerId)}
            avatar={<img src={'data:image/png;base64,' + poster.profilePictureBase64} className='profilePicture' alt='Profile' />}
            title={'Posted by ' + poster.displayName}
          />
          <Button className='optionsButton' aria-controls='simple-menu' aria-haspopup='true' onClick={handleClick}>
            Open Menu
          </Button>
        </div>
        {imgData ? <CardMedia className={classes.media} image={'data:image/png;base64,' + imgData} /> : null}

        <CardContent>
          <Typography variant='body2' color='textSecondary' component='p'>
            <b>{poster.displayName}:</b> {subtitle}
          </Typography>
        </CardContent>
        <CardActions disableSpacing>
          <IconButton
            className={clsx(classes.expand, {
              [classes.expandOpen]: expanded
            })}
            onClick={handleExpandClick}
            aria-expanded={expanded}
            aria-label='Show comments'
          >
            <ExpandMoreIcon />
          </IconButton>
        </CardActions>
        <Collapse in={expanded} timeout='auto' unmountOnExit>
          <WriteCommentBar postId={postId} />
          <Commentlist comments={comments} />
        </Collapse>
      </Card>

      <Menu id='simple-menu' anchorEl={anchorEl} keepMounted open={Boolean(anchorEl)} onClose={handleClose}>
        <MenuItem onClick={() => setShowReportDialog(true)}>Report</MenuItem>
      </Menu>

      <ReportDialog open={showReportDialog} postId={postId} />
    </>
  )
}
