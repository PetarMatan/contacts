<template>
    <div>
        <div class="input-group col-xl-6 centered">
            <div class="input-group-prepend">
                <span class="input-group-text" id="">First and last name</span>
            </div>
            <input type="text" class="form-control" v-model="name">
            <input type="text" class="form-control" v-model="surname">
        </div>
        <div class="input-group col-xl-6 centered">
            <div class="input-group-prepend">
                <span class="input-group-text" id="">Address</span>
            </div>
            <input type="text" class="form-control" v-model="address">
        </div>
        <div class="input-group col-xl-6 centered">
            <div class="input-group-prepend">
                <span class="input-group-text" id="">Email</span>
            </div>
            <input type="text" class="form-control" v-model="email">
        </div>
        <div class="input-group col-xl-6 centered">
            <div class="input-group-prepend">
                <span class="input-group-text" id="">Mobile number</span>
            </div>
            <input type="text" class="form-control" v-model="mobileNumber">
            <button class="btn btn-primary" v-on:click="onMobileNumberChange()" >Save number</button>
        </div>
        <div v-for="mobileNumber in mobileNumbers" :key="mobileNumber">
           <div class="input-group col-xl-6 centered">
            <div class="input-group-prepend">
                <span class="input-group-text" id="">Mobile number</span>
            </div>
            <input type="text" class="form-control" disabled="disabled" v-bind:value="mobileNumber" />
            <button class="btn btn-primary" v-on:click="deleteMobileNumber(mobileNumber)">Delete number</button>
            </div>
        </div>
        <div class="input-group col-xl-6 centered">
            <div class="input-group-prepend">
                <span class="input-group-text" id="">Contact tag</span>
            </div>
            <input type="text" class="form-control" v-model="contactTag">
            <button class="btn btn-primary" v-on:click="onContactTagChange()" >Save contact tag</button>
        </div>
        <div v-for="currentContactTag in contactTags" :key="currentContactTag.value">
           <div class="input-group col-xl-6 centered">
            <div class="input-group-prepend">
                <span class="input-group-text" id="">Contact tag</span>
            </div>
            <input type="text" class="form-control" disabled="disabled" v-bind:value="currentContactTag.value" />
            <button class="btn btn-primary" v-on:click="deleteContactTag(currentContactTag)">Delete tag</button>
            </div>
        </div>
        <button class="btn btn-success sim-save mt-2" v-on:click = "saveContact()">Save contact</button>
    </div>
</template>

<script>
import contactService from '@/api-services/contactService'
import newPhoneContactService from '@/services/newPhoneContactService'

export default{
    'name': "NewPhoneContact",
    components: {
        contactService
    },
    props: ['guid'],
    data(){
        return {
            'name': '',
            'surname': '',
            'mobileNumber': "",
            'mobileNumbers': [],
            'address': '',
            'email': '',
            'contactTag' : '',
            'contactTags': []
        }
    },
    created(){
        if (this.guid) {
            contactService.get(this.guid).then((response) => {
            this.name = response.data.name || '';
            this.surname = response.data.surname || '';
            response.data.mobileNumbers.forEach(numberObject => {
                this.mobileNumbers.push(numberObject.number);
            })
            this.address = response.data.address || "";
            this.email = response.data.email || ""; 
            response.data.contactTags.forEach(tagObject => {
                this.contactTags.push({
                    'id': tagObject.id,
                    'value': tagObject.tag
                });
            });
        }).catch((error) => {
            new Error(error);
            })
        }
    },
    methods: {
        saveContact(){
            contactService.createOrEdit(this.guid, {
                'name': this.name,
                'surname': this.surname,
                'mobileNumbers': this.mobileNumbers,
                'address': this.address,
                'email': this.email,
                'contactTags': this.contactTags,
                'contactType': 'phone',
                'guid': this.guid
            }).then(response => {
                alert("Contact is saved");
            }).catch(error => {
                alert("Contact saving failed");
            })
        },
        onMobileNumberChange(){
            this.mobileNumbers.push(this.mobileNumber);
            this.mobileNumber = "";
        },
        onContactTagChange(){
            this.contactTags.push({
                'id': 0,
                'value': this.contactTag
            })
            this.contactTag = "";
        },
        deleteMobileNumber(mobileNumber){
            this.mobileNumbers = this.mobileNumbers.filter(arrayNumber => {
                return arrayNumber != mobileNumber;
            });
        },
        deleteContactTag(contactTag){
            this.contactTags = this.contactTags.filter(contactTagObject => {
                return contactTagObject.value != contactTag.value;
            });
        }
    }
}
</script>

<style>
    .centered{
        margin: auto;
        margin-top:10px;
    }
</style>