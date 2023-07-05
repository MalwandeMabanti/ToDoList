import axios from 'axios';


const API_URL = 'https://localhost:7142/api/Authentication'; // Replace with your API's URL

export default {
    register(userData) {
        return axios.post(`${API_URL}/Register`, userData);
    },

    // Other user-related API calls can go here
};
