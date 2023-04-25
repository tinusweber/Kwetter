import React, { useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Card from '@material-ui/core/Card'
import CardActionArea from '@material-ui/core/CardActionArea'
import CardActions from '@material-ui/core/CardActions'
import CardContent from '@material-ui/core/CardContent'
import CardMedia from '@material-ui/core/CardMedia'
import Button from '@material-ui/core/Button'
import Typography from '@material-ui/core/Typography'
import TextField from '@material-ui/core/TextField'
import ImageUploader from 'react-images-upload'
import './WritepostBar.css'
import SendIcon from '@material-ui/icons/Send'
import authService from '../../services/auth.service'
import { useAlert } from 'react-alert'
import PostService from '../../services/post.service'
const useStyles = makeStyles({
  root: {
    width: 480,
    marginBottom: '40px',
    borderRadius: 10
  },
  media: {
    height: 140
  }
})

export default function WritepostBar({ onclick }) {
  const changeimage = (input) => {
    let reader = new FileReader()
    reader.readAsDataURL(input[0])
    reader.onloadend = () => {
      setImage(reader.result.replace('data:image/png;base64,', ''))
    }
  }
  const [image, setImage] = useState('')
  const classes = useStyles()
  const [postText, setPostText] = useState('')
  let postedBy = {}
  authService.getCurrentUser().then((data) => (postedBy = data))
  const alert = useAlert()

  const addPost = () => {
    let post = {}
    post.imgData = image
    post.body = postText
    post.postedBy = postedBy
    PostService.addPost(post)
      .then((res) => {
        document.location.reload()
      })
      .catch((err) => {
        alert.error(err.response?.data?.message)
      })
  }

  return (
    <Card className={classes.root}>
      <CardActionArea>
        {image ? <CardMedia className={classes.media} image={image} title='uploaded image' /> : null}

        <CardContent>
          <form data-testid='postInput'>
            <Typography gutterBottom variant='h5' component='h2'>
              {postText}
            </Typography>
            <TextField
              className='textInput'
              id='help'
              data-testid='dikkevettepeace'
              multiline
              variant='outlined'
              placeholder='Today i feel like...'
              onChange={(e) => setPostText(e.target.value)}
            />
          </form>
        </CardContent>
      </CardActionArea>
      <CardActions>
        <div className='itemContainer'>
          <Button
            className='sendButton'
            variant='contained'
            color='primary'
            id='submitbtn'
            data-testid='submitBtn'
            onClick={() => addPost()}
            endIcon={<SendIcon />}
          >
            Send
          </Button>

          <ImageUploader
            className='imageUpload'
            withIcon={false}
            buttonText='Choose images'
            onChange={(image) => {
              changeimage(image)
            }}
            withLabel={false}
            imgExtension={['.jpg', '.gif', '.png', '.gif', '.jpeg']}
            maxFileSize={5242880}
            singleImage={true}
          />
        </div>
      </CardActions>
    </Card>
  )
}
