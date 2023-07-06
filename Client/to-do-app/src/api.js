import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'https://localhost:7142/api', 
    withCredentials: false,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
    },
});

apiClient.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    console.log("Token sent with a request:", token);
    config.headers['Authorization'] = `Bearer ${token}`;
    return config;
})

export default {
    getTodos() {
        const token = localStorage.getItem('token');
        console.log("Token at the time of getTodos():", token)
        return apiClient.get('/Todos');
    },

    createTodo(newTodo) {
        return apiClient.post('/Todos', newTodo)
    },

    updateTodo(todo) {
        return apiClient.put(`/Todos/${todo.id}`, JSON.stringify(todo));
    },

    removeTodo(todo) {
        return apiClient.delete(`/Todos/${todo.id}`)
    }
};





