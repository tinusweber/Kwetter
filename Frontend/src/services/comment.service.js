import axios from 'axios'

const apiUrl = process.env.REACT_APP_API_URL + '/Comment'
const token = JSON.parse(localStorage.getItem('user'))?.access_token
const instance = axios.create({
  baseURL: apiUrl,
  timeout: 1000,
  headers: { Authorization: 'Bearer ' + token }
})

class CommentService {
  AddComment(postid, body) {
    return instance
      .post('', {
        tweetId: postid,
        body: body
      })
      .then((response) => {
        return response.data
      })
  }

  GetAll() {
    return instance.get().then((response) => {
      return response.data
    })
  }

  GetById(id) {
    return instance.get(id).then((response) => {
      return response.data
    })
  }
  GetByPost(id) {
    return instance.get('', { params: { tweetId: id } }).then((response) => {
      return response.data
    })
  }

  GetByUser(id) {
    return instance.get('userId=' + id).then((response) => {
      return response.data
    })
  }

  GetByUserAndPost(userId, postId) {
    return instance.get('userId=' + userId + '&postId=' + postId).then((response) => {
      return response.data
    })
  }

  DeleteById(id) {
    return instance.delete(id).then((response) => {
      return response.data
    })
  }
}

export default new CommentService()
