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
    config.headers['Authorization'] = `Bearer ${token}`;

    if (config.data instanceof FormData) {
        config.headers['Content-Type'] = 'multipart/form-data';
    } else {
        config.headers['Content-Type'] = 'application/json';
        config.data = JSON.stringify(config.data);
    }

    return config;
})

export default {

    getAllTodos() {
        return apiClient.get('/Todos/all');
    },

    getTodos() {
        
        return apiClient.get('/Todos');
    },

    createTodo(formData) {
        return apiClient.post('/Todos', formData);
    },

    updateTodo(formData) {
        return apiClient.put(`/Todos/${formData.get('id')}`, formData);
    },

    removeTodo(todo) {
        return apiClient.delete(`/Todos/${todo.id}`);
    }
};





