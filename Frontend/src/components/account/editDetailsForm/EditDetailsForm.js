import React, { useState } from 'react'
import { TextField } from '@material-ui/core'
import { useEffect } from 'react'
import './EditDetailsForm.css'
export default function EditDetailsForm({ accountDetails, callback }) {
  const [username, setUsername] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [oldPassword, setOldPassword] = useState('')
  const [passwordConfirm, setPasswordConfirm] = useState('')
  const [bio, setBio] = useState('')

  useEffect(() => {
    callback({
      username: username,
      email: email,
      oldPass: oldPassword,
      password: password,
      passwordConfirm: passwordConfirm,
      bio: bio
    })
  }, [username, email, passwordConfirm, oldPassword, password, bio, callback])

  useEffect(() => {
    setUsername(accountDetails.displayName)
    setEmail(accountDetails.email)
    setBio(accountDetails.biography)
  }, [accountDetails])

  return (
    <div id='editDetailsForm'>
      <div className='DetailsFormItem'>
        <TextField id='standard-basic' label='Display name' value={username} onChange={(e) => setUsername(e.target.value)} />
      </div>
      <div className='DetailsFormItem'>
        <TextField id='standard-basic' label='Bio' rows={3} value={bio} onChange={(e) => setBio(e.target.value)} />
      </div>
      <div className='DetailsFormItem'>
        <TextField id='standard-basic' label='Current Password' type='password' onChange={(e) => setOldPassword(e.target.value)} />
      </div>
      <TextField
        id='standard-basic'
        label='Password'
        type='password'
        placeholder='Leave empty to keep current password'
        onChange={(e) => setPassword(e.target.value)}
      />

      <div className='DetailsFormItem'>
        <TextField id='standard-basic' label='Confirm Password' type='password' onChange={(e) => setPasswordConfirm(e.target.value)} />
      </div>
    </div>
  )
}
