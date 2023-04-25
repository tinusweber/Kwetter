import axios from 'axios'
const API_URL = process.env.REACT_APP_API_URL
const instance = axios.create({
  baseURL: API_URL,
  timeout: 5000
})
class AuthService {
  login(username, password) {
    const params = new URLSearchParams()
    params.append('grant_type', 'password')
    params.append('client_id', 'secret_user_client_id')
    params.append('client_secret', 'secret')
    params.append('scope', 'apiscope')
    params.append('username', username)
    params.append('password', password)

    return instance.post('/connect/token', params).then((response) => {
      if (response.data.access_token) {
        localStorage.setItem('user', JSON.stringify(response.data))
      }

      return response.data
    })
  }

  logout() {
    localStorage.removeItem('user')

    window.location.reload()
  }

  register(username, email, password) {
    return instance.post(API_URL + '/Auth', {
      Username: username,
      Email: email,
      Password: password
    })
  }
  async isLoggedin() {
    var token = JSON.parse(localStorage.getItem('user'))
    if (token) {
      return fetch(process.env.REACT_APP_API_URL + '/Profile', {
        headers: {
          Authorization: 'Bearer ' + token.access_token
        }
      }).then((response) => {
        if (response) {
          return true
        }
        return false
      })
    }
  }
  async getCurrentUser() {
    var token = JSON.parse(localStorage.getItem('user'))
    if (token) {
      return fetch(process.env.REACT_APP_API_URL + '/Profile', {
        headers: {
          Authorization: 'Bearer ' + token.access_token
        }
      })
        .then((response) => {
          if (response) {
            const json = response.json()
            return json
          }
        })
        .catch((error) => {
          this.logout()
        })
    }
  }
}

export default new AuthService()
