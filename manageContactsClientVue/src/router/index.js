import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import NewSimContact from '@/components/NewSimContact'
import NewPhoneContact from '@/components/NewPhoneContact'
import NotFound from '@/components/ErrorHandling/NotFound'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/newSimContact',
      name: 'newSimContact',
      component: NewSimContact
    },
    {
      path: '/newPhoneContact',
      name: 'newPhoneContact',
      component: NewPhoneContact
    },
    {
      path: '/editSimContact/:guid',
      name: 'editSimContact',
      component: NewSimContact,
      props: true
    },
    {
      path: '/editPhoneContact/:guid',
      name: 'editPhoneContact',
      component: NewPhoneContact,
      props: true
    },
    {
      path: '*',
      name: 'NotFound',
      component: NotFound
    }
  ]
})
