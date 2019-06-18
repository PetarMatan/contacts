import Axios from 'axios'

const RESOURCE_NAME = '/contacts'
const NEW_CONTACT = '/new'
const DELETE_CONTACT = '/delete'
const EDIT_CONTACT = '/edit'
const MOBILE_NUMBERS = '/mobileNumbers'
const GET = '/get'

export default {
  getAll() {
    return Axios.get(RESOURCE_NAME)
  },
  get(guid) {
    return Axios.get(`${RESOURCE_NAME}/${guid}`)
  },
  create(data) {
    return Axios.post(RESOURCE_NAME + NEW_CONTACT, data)
  },
  update(id, data) {
    return Axios.post(RESOURCE_NAME + NEW_CONTACT, data)
  },
  delete(guid) {
    return Axios.get(`${RESOURCE_NAME + DELETE_CONTACT}/${guid}`)
  },
  edit(data) {
    return Axios.post(RESOURCE_NAME + EDIT_CONTACT, data)
  },
  createOrEdit(guid, data) {
    return guid ? this.edit(data) : this.create(data)
  },
  filterByMobileNumber(number) {
    return Axios.get(`${MOBILE_NUMBERS + GET}/${number}`)
  }
}
