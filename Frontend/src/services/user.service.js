import axios from 'axios'

const apiUrl = process.env.REACT_APP_API_URL
const token = JSON.parse(localStorage.getItem('user'))?.access_token
const instance = axios.create({
  baseURL: apiUrl,
  timeout: 5000,
  headers: { Authorization: 'Bearer ' + token }
})

class UserService {
  getById(id) {
    return instance.get('Profile/' + id).then((response) => {
      return response.data
    })
  }

  getAll() {
    return instance.options('Profile').then((response) => {
      return response.data
    })
  }

  updatePassword(oldPass, newPass, newPassConfirm) {
    return instance
      .put('Auth', {
        new: newPass,
        newConfirm: newPassConfirm,
        current: oldPass
      })
      .then((res) => {
        return res.data
      })
  }
  updateUserDetails(userDetails) {
    return instance
      .put('Profile', {
        displayName: userDetails.username,
        profilePictureBase64: userDetails.profilePicture,
        biography: userDetails.bio
      })
      .then((res) => {
        return res.data
      })
  }
}

export default new UserService()
