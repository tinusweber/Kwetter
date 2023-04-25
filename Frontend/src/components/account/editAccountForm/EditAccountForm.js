import { Button } from '@material-ui/core'
import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import userService from '../../../services/user.service'
import EditDetailsForm from '../editDetailsForm/EditDetailsForm'
import ProfilePictureEditor from '../editProfilePicture/ProfilePictureEditor'
import { useAlert } from 'react-alert'
import './editAccountForm.css'
export default function EditAccountForm({ accountDetails }) {
  const [profilePictureData, setProfilePictureData] = useState('')
  const [details, setDetails] = useState({})
  const alert = useAlert()

  function setProfilePicData(data) {
    if (data.includes('data:image/png;base64,')) {
      data = data.replace('data:image/png;base64,', '')
    }

    setProfilePictureData(data)
  }
  useEffect(() => {
    setProfilePictureData(accountDetails.profilePictureBase64)
  }, [accountDetails])

  const saveChanges = () => {
    details.profilePicture = profilePictureData
    if (details.password) {
      userService
        .updatePassword(details.oldPass, details.password, details.passwordConfirm)
        .catch((err) => alert.error(err.response.data.message))
    }
    userService
      .updateUserDetails(details)
      .then((reply) => {
        alert.success('Profile is updates, refresh to view changes')
      })
      .catch((err) => alert.error(err.response.data.message))
  }
  return (
    <div id='formContainer'>
      <img src={'data:image/png;base64,' + profilePictureData} id='profilePicture' alt='profile' />
      <ProfilePictureEditor value={profilePictureData} callback={setProfilePicData} />
      <EditDetailsForm accountDetails={accountDetails} callback={setDetails} />

      <Button onClick={saveChanges}>Save changes</Button>
    </div>
  )
}
