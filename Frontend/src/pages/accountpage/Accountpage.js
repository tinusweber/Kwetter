import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import EditAccountForm from '../../components/account/editAccountForm/EditAccountForm'
import authService from '../../services/auth.service'

export default function Accountpage() {
  const [accountDetails, setAccountDetails] = useState({})
  useEffect(() => {
    authService.getCurrentUser().then((user) => setAccountDetails(user))
  }, [])
  return (
    <div>
      <div>
        <EditAccountForm accountDetails={accountDetails} />
      </div>
    </div>
  )
}
