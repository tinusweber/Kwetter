import React from 'react'
import { useHistory } from 'react-router-dom'
import { Button } from '@material-ui/core'
import AuthService from '../../../services/auth.service'
const LogoutButton = () => {
  let history = useHistory()
  return (
    <Button
      variant='contained'
      onClick={() => {
        AuthService.logout()
        history.replace('/')
      }}
    >
      Log out
    </Button>
  )
}

export default LogoutButton
