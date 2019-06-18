<template>
    <tr>
      <th>1</th>
      <td>{{contactData.name}}</td>
      <td>{{contactData.surname}}</td>
      <td style="cursor: pointer" v-on:click="editContact()">Edit</td>
      <td style="cursor: pointer" v-on:click="deleteContact()">Delete</td>
    </tr>
</template>
<script>
import contactService from '@/api-services/contactService'
import MobileNumber from '@/components/MobileNumber'
import Router from '@/router/index'
export default{
    name:'Contact',
    data(){
      return {
        contactData: this.contact,
      }
    },
    methods: {
      deleteContact(){
        contactService.delete(this.contactData.guid).then((response) => {
          this.$emit('contactDeleted', response.data);
        }).catch((error) => {
           new Error(error);
        });
      },
      editContact(){
        let route = this.contactData.contactType == "sim" ? '/editSimContact/' : '/editPhoneContact/';
        Router.push(route + this.contactData.guid);
      }
    },
    props: ['contact'],   
}
</script>