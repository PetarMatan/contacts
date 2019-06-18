import contactService from '@/api-services/contactService'

export default{
  onPhoneContactCreated () {
    contactService.get()
  }
}
