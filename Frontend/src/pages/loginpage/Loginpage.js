import React, { useState } from 'react'
import Avatar from '@material-ui/core/Avatar'
import Button from '@material-ui/core/Button'
import CssBaseline from '@material-ui/core/CssBaseline'
import TextField from '@material-ui/core/TextField'
import Alert from '@material-ui/lab/Alert'
import { useHistory } from 'react-router-dom'
import Link from '@material-ui/core/Link'
import Paper from '@material-ui/core/Paper'
import Grid from '@material-ui/core/Grid'
import LockOutlinedIcon from '@material-ui/icons/LockOutlined'
import Typography from '@material-ui/core/Typography'
import { makeStyles } from '@material-ui/core/styles'
import AuthService from '../../services/auth.service'
import background from '../../background.jpg'

const useStyles = makeStyles((theme) => ({
  base: {
    height: '100vh'
  },

  paper: {
    margin: theme.spacing(8, 4),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center'
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main
  },
  form: {
    width: '100%', // Fix IE 11 issue.
    marginTop: theme.spacing(1)
  },
  submit: {
    margin: theme.spacing(3, 0, 2)
  }
}))

export default function Loginpage(props) {
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [message, setMessage] = useState('')

  const history = useHistory()
  const classes = useStyles()
  const handleLogin = (e) => {
    e.preventDefault()

    setMessage('')

    AuthService.login(username, password)
      .then(() => {
        document.location.replace('/')
      })
      .catch((error) => {
        const resMessage = (error.response && error.response.data && error.response.data.message) || error.message || error.toString()

        setMessage(resMessage)
        console.log(resMessage)
      })
  }
  return (
    <Grid container component='main' className={classes.base}>
      <CssBaseline />
      <Grid item xs={false} sm={4} md={7}>
        <img src={background} alt='background' />
      </Grid>
      <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
        <div className={classes.paper}>
          <Avatar className={classes.avatar}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component='h1' variant='h5'>
            Sign in
          </Typography>
          <form className={classes.form} noValidate data-testid='login-form'>
            <TextField
              variant='outlined'
              margin='normal'
              required
              fullWidth
              id='username'
              label='username'
              name='username'
              autoComplete='username'
              autoFocus
              onChange={(e) => setUsername(e.target.value)}
            />
            <TextField
              variant='outlined'
              margin='normal'
              required
              fullWidth
              name='password'
              label='Password'
              type='password'
              id='password'
              onChange={(e) => setPassword(e.target.value)}
              autoComplete='current-password'
            />

            <Button
              type='submit'
              id='submitButton'
              fullWidth
              variant='contained'
              color='primary'
              className={classes.submit}
              onClick={handleLogin}
            >
              Sign In
            </Button>
            <Grid container>
              <Grid item>
                <Link variant='body2' onClick={() => history.push('/register')}>
                  Dont already have an account? Sign up
                </Link>
              </Grid>
            </Grid>
            {message ? (
              <Alert severity='error' id='BadLoginAlert' data-testId='badLoginAlert'>
                {message}
              </Alert>
            ) : null}
          </form>
        </div>
      </Grid>
    </Grid>
  )
}
