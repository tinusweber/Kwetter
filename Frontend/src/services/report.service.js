import axios from 'axios'

const apiUrl = process.env.REACT_APP_API_URL + '/Report'
const token = JSON.parse(localStorage.getItem('user'))?.access_token
const instance = axios.create({
  baseURL: apiUrl,
  timeout: 1000,
  headers: { Authorization: 'Bearer ' + token }
})

class ReportService {
  Add(postid, reason) {
    return instance
      .post('', {
        postId: postid,
        reason: reason
      })
      .then((response) => {
        return 'Thank you for your report'
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

  DeleteById(id) {
    return instance.delete(id).then((response) => {
      return response.data
    })
  }

  UpdateById(id, status, reason) {
    return instance
      .patch(id, {
        reportStatus: status,
        reason: reason
      })
      .then((response) => {
        return response.data
      })
  }
}

export default new ReportService()
