import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'https://localhost:7142/api', // Replace with your API's base URL
    withCredentials: false,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
    },
});

export default {
    getTodos() {
        return apiClient.get('/Todos');
    },

    createTodo(newTodo) {
        return apiClient.post('/Todos', newTodo)
    },

    updateTode(todo) {
        return apiClient.post(`/Todos/${todo.id}`)
    },

    removeTodo(todo) {
        return apiClient.delete(`/Todos/${todo.id}`)
    }
    // other methods for POST, PUT, DELETE etc. if required
    
};





