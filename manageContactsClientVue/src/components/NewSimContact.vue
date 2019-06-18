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
        <button class="btn btn-success sim-save mt-2" v-on:click = "saveContact()">Save contact</button>
    </div>
</template>

<script>
import contactService from '@/api-services/contactService'

export default{
    'name': "NewSimContact",
    components: {
        contactService
    },
    props: ['guid'],
    data(){
        return {
            'dbContactData': {},
            'name': '',
            'surname': '',
            'mobileNumber': "",
            'mobileNumbers': []
        }
    },
    created(){
        if(this.guid){
            contactService.get(this.guid).then((response) => {
                console.log("this is response", response.data);
                this.name = response.data.name || "";
                this.surname = response.data.surname || "";
                response.data.mobileNumbers.forEach(numberObject => {
                    this.mobileNumbers.push(numberObject.number);
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
                'guid': this.guid,
                'contactType': 'sim'
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
        deleteMobileNumber(mobileNumber){
            this.mobileNumbers = this.mobileNumbers.filter(arrayNumber => {
                return arrayNumber != mobileNumber;
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