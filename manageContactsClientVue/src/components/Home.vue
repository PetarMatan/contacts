<template>
  <div>
    <input
      @input="filterByMobileNumber"
      placeholder="Filter by mobile numbers.."
      type="text"
      class="form-control col-xl-3 centered mt-2"
    >
    <table class="table col-xl-8" style="margin: auto; margin-top:10px; border:1px solid black">
      <thead>
        <tr>
          <th>#</th>
          <th>First</th>
          <th>Last</th>
          <th>Action</th>
          <th>Action</th>
        </tr>
      </thead>
      <tbody>
        <Contact
          @contactDeleted="onContactDelete"
          v-for="contact in contacts"
          :key="contact.guid"
          :contact="contact"
        />
      </tbody>
    </table>
    <button
      v-on:click="createNewContact(newSimContact)"
      class="btn btn-primary mt-1"
    >New sim contact</button>
    <button
      v-on:click="createNewContact(newPhoneContact)"
      class="btn btn-primary mt-1"
    >New phone contact</button>
  </div>
</template>
<script>
import contactService from "@/api-services/contactService";
import Contact from "@/components/Contact";
import Router from "@/router/index";
let callback = undefined;

export default {
  name: "Home",
  components: {
    Contact
  },
  data() {
    return {
      contacts: [],
      newSimContact: "newSimContact",
      newPhoneContact: "newPhoneContact",
      initialContacts: []
    };
  },
  created() {
    this.onComponentCreated();
  },
  methods: {
    onComponentCreated() {
      contactService
        .getAll()
        .then(response => {
          this.initialContacts = this.contacts = response.data || []; //glupo, referenca a ne kopija?
        })
        .catch(error => {
          new ErrorEvent("Failed while fetching contacts", error);
        });
    },
    createNewContact(path) {
      Router.push({
        name: path
      });
    },
    onContactDelete(data) {
      this.contacts = data;
    },
    filterByMobileNumber(e) {
      if (callback) clearTimeout(callback);
      callback = setTimeout(() => {
        let number = e.target.value;
        if (number == "") {
          this.contacts = this.initialContacts;
          return;
        }
        contactService.filterByMobileNumber(number).then(response => {
          this.contacts = [response.data ? response.data : {}];
        });
      }, 400);
    }
  }
};
</script>
<style>
.centered {
  margin: auto;
}
</style>