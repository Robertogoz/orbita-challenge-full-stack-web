import Vue from 'vue'
import VueRouter from 'vue-router'
import Register from '../views/Register.vue'
import Users from '../views/Users.vue'
import Edit from '../views/Edit.vue'

Vue.use(VueRouter)

const routes = [
  {
    path:'/',
    name: 'User',
    component: Users
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/edit/:id',
    name: 'Edit',
    component: Edit
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
