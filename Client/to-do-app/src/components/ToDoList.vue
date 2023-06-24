<template>
    <div>
        <input v-model="newTodo" type="text" placeholder="New Todo"/>
        <button @click="addTodo">Add Todo</button>

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
                todos: [],
                newTodo: ''
            };
        },

        methods: {
            async addTodo() {
                const response = await api.createTodo({ title: this.newTodo });
                this.todos.push(response.data);
                this.newTodo;
            },
        },

        async created() {
            const response = await api.getTodos();
            this.todos = response.data;
        },
    };
</script>