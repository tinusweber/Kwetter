import axios from 'axios'

const API_URL = process.env.REACT_APP_API_URL + '/tickets'

const token = JSON.parse(localStorage.getItem('user'))?.access_token
const instance = axios.create({
  baseURL: API_URL,
  timeout: 1000,
  headers: { Authorization: 'Bearer ' + token }
})

class TicketService {
  addTicket(title, description) {
    instance
      .post('', {
        title: title,
        body: description
      })
      .then((response) => {
        return response.data
      })
  }

  getTicketsByStatusAndCustomer(status, client) {
    return instance.get('?client=' + client + '&status=' + status).then((response) => {
      return response.data
    })
  }

  getUnawnseredTickets(client) {
    let inProgress = []
    let pending = []

    return instance.get('?client=' + client + '&status=IN_PROGRESS').then((response) => {
      inProgress = response.data
      return instance.get('?client=' + client + '&status=PENDING').then((res) => {
        pending = res.data

        var joined = pending.concat(inProgress)
        return joined
      })
    })
  }
  replyToTicket(ticketId, reply) {
    return instance
      .post('/reply', {
        ticketId: ticketId,
        replyBody: reply
      })
      .then((response) => {
        return response.data
      })
  }
}

export default new TicketService()
