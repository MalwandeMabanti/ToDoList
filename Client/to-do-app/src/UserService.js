import axios from 'axios';

const API_URL = 'https://localhost:7142/api/Authentication'; 

function setAuthToken(token) {
    if (token) {
        axios.defaults.headers.common['Authorization'] = 'Bearer ' + token;
    } else {
        delete axios.defaults.headers.common['Authorization'];
    }
}

const UserService = {
    register(userData) {
        return axios.post(`${API_URL}/Register`, userData);
    },

    login(userData) {
        return axios.post(`${API_URL}/Login`, userData)
            .then(response => {
                if (response.data.token) {

                    console.log('Token received from the server:', response.data.token)
                    
                    setAuthToken(response.data.token);

                    localStorage.setItem('token', response.data.token);

                    console.log('Token stored in local storage:', localStorage.getItem('token'))
                }
                return response;
            });
    },

    // Other user-related API calls can go here
};

export default UserService;


