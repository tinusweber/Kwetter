import React, { useState, useEffect } from 'react'
import userService from '../../../../services/user.service'
import './FollowButton.css'
export default function FollowButton({ userDetails }) {
  const [following, setFollowing] = useState(false)

  useEffect(() => {
    if (userDetails.username) {
      userService.isFollowing(userDetails.username).then((res) => {
        setFollowing(res)
      })
    }
  }, [userDetails])

  const unFollow = () => {
    userService.unFollowUserByName(userDetails.username).then((res) => {
      setFollowing(false)
    })
  }
  const Follow = () => {
    userService.followUserByName(userDetails.username).then((res) => {
      setFollowing(true)
    })
  }

  return following ? (
    <button onClick={unFollow} className='unfollow-button'>
      unFollow
    </button>
  ) : (
    <button onClick={Follow} className='follow-button'>
      Follow
    </button>
  )
}
