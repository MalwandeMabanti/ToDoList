import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'
import LoginForm from './components/LoginForm.vue'
import RegistrationForm from './components/RegistrationForm.vue'
import ToDoList from './components/ToDoList.vue'

// define your routes
const routes = [
    { path: '/', component: LoginForm },
    { path: '/login', component: LoginForm },
    { path: '/register', component: RegistrationForm },
    { path: '/todolist', component: ToDoList }
    
]

// create the router instance
const router = createRouter({
    history: createWebHistory(),
    routes,
})

// create and mount the app
createApp(App)
    .use(router)
    .mount('#app')
