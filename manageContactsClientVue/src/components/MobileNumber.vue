<template>
    <div>
        <input type="text" class="input-medium bfh-phone" data-country="US" v-bind:value="mobileNumber.number">
    </div>
</template>


<script>

export default{
    name:'MobileNumber',
    props: ['mobileNumber']
}

</script>