import React from 'react'
import './Profile.css'
import { useHistory } from 'react-router-dom'
import { Typography, MenuItem, Menu } from '@material-ui/core'
import authService from '../../../services/auth.service'
const Profile = ({ user }) => {
  const [anchorEl, setAnchorEl] = React.useState(null)

  const handleClick = (event) => {
    setAnchorEl(event.currentTarget)
  }

  const handleClose = () => {
    setAnchorEl(null)
  }
  const history = useHistory()
  return (
    <div>
      <div className='profile'>
        <div
          onClick={(e) => {
            handleClick(e)
          }}
        >
          <img src={'data:image/png;base64,' + user?.profilePictureBase64} className='profilePicture' alt='avatar' />
          <Typography data-testid='profileName' className='item'>
            {user?.displayName}
          </Typography>
        </div>

        <Menu id='simple-menu' anchorEl={anchorEl} keepMounted open={Boolean(anchorEl)} onClose={handleClose}>
          <MenuItem onClick={() => history.push('/user/' + user.OwnerId)}>Profile</MenuItem>
          <MenuItem onClick={() => history.push('/account')}>My account</MenuItem>
          <MenuItem onClick={() => authService.logout()}>Logout</MenuItem>
        </Menu>
      </div>
    </div>
  )
}

export default Profile
