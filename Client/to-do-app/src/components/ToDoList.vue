<template>
    <div>
        <ul>
            <li v-for="todo in todos" :key="todo.id">
                {{todo.title}}
            </li>
        </ul>
    </div>
</template>

<script>
    import api from '../api';  

    export default {
        data() {
            return {
                todos: []
            };
        },
        async created() {
            const response = await api.getTodos();
            this.todos = response.data;
        },
    };
</script>