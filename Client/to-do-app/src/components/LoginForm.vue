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

    export default {
        name: "LoginForm",
    }

</script>

