<template>
    <div>
        <div>
            <h2>Login</h2>
            <form @submit.prevent="login">
                <label>
                    <input v-model="loginEmail" type="email" />
                </label>
                <br />
                <label>
                    <input v-model="loginPassword" type="password" />
                </label>
                <br />
                <button type="submit">Log in</button>
            </form>
            <router-link to="/register">Don't have an account? Register</router-link>
        </div>

       
    </div>
</template>

<script>
    import UserService from '../UserService';

    export default {
        name: "LoginForm",
        data() {
            return {
                loginEmail: '',
                loginPassword: ''
            };
        },
        methods: {
            async login() {
                try {
                    const response = await UserService.login({
                        Email: this.loginEmail,
                        Password: this.loginPassword,
                    });
                    console.log(response, "And something else");
                    console.log(response.data.token, "This is your token")

                    if (response.status === 200) {
                        this.$router.push('/todolist');
                    }
                    else {
                        this.$router.push('/');
                    }
                    return response.status; // Success status code, e.g., 200

                    
                } catch (error) {
                    if (error.response) {
                        // The request was made and the server responded with a status code
                        // that falls out of the range of 2xx
                        console.log(error.response.data, "Data");
                        console.log(error.response.status, "Status");
                        console.log(error.response.headers, "Headers");
                        return error.response.status; // Error status code, e.g., 400, 401, 404, 500 etc.
                    } else if (error.request) {
                        // The request was made but no response was received
                        console.log(error.request, "Request");
                        return null; // or any other value indicating that no response was received
                    } else {
                        // Something happened in setting up the request that triggered an Error
                        console.log('Error', error.message);
                        return null; // or any other value indicating that an error occurred
                    }


                }
                
            }
            
        }
            
                
    }

</script>

