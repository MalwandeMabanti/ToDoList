<template>
    <div>
        <h2>Register</h2>
        <form @submit.prevent="register">
            <label>
                <input v-model="registerEmail" type="email" placeholder="Email" />
            </label>
            <br />
            <label>
                <input v-model="registerPassword" type="password" placeholder="Password" />
            </label>
            <br />
            <label>
                <input v-model="conformPassword" type="password" placeholder="Confirm Password" />
            </label>
            <br />
            <button type="submit">Register</button>
            <p>Already have an account? <router-link to="/login">Login</router-link></p>
        </form>
    </div>
</template>

<script>
    import UserService from '../UserService';

    export default {
      name: "RegistrationForm",
      data() {
        return {
          registerEmail: "",
          registerPassword: "",
          conformPassword: ""
        };
      },
      methods: {
        register() {
          if (this.registerPassword !== this.conformPassword) {
            alert("Passwords do not match!");
            return;
          }

          UserService.register({
              Email: this.registerEmail,
              Password: this.registerPassword,
              ConfirmPassword: this.conformPassword
          })
          .then(response => {
            console.log(response);
         
          })
          .catch(error => {
        if (error.response) {
            // The request was made and the server responded with a status code
            // that falls out of the range of 2xx
            console.log(error.response.data, "Data");
            console.log(error.response.status, "Status");
            console.log(error.response.headers, "Headers");
        } else if (error.request) {
            // The request was made but no response was received
            console.log(error.request, "Request");
        } else {
            // Something happened in setting up the request that triggered an Error
            console.log('Error', error.message);
        }
        console.log(error.config, "Config");
    });
        },
      },
    };
</script>