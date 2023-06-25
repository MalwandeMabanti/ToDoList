<template>
    <h2>Add a ToDo</h2>
    <div>
        <form @submit.prevent="addTodo">
            <input v-model="newTodo.title" type="text" placeholder="New Todo Title" />
            <input v-model="newTodo.description" type="text" placeholder="New Todo Description" />
            <button type="submit">Add Todo</button>
        </form>
        <br />
        <br />
        <table>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Completed</th>
                    <th>Edit</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="todo in todos" :key="todo.id">
                    <td>{{ todo.title }}</td>
                    <td>{{ todo.description }}</td>
                    <td>
                        <input type="checkbox" @change="removeTodo(todo)" />
                    </td>
                    <td>
                        <button v-if="!todo.isEditing" @click="editTodo(todo)">Edit</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import api from '../api';  

    export default {
        data() {
            return {
                todos: [],
                newTodo: {
                    title: '',
                    description: '',
                }
            };
        },

        methods: {
            addTodo() {
                if (this.newTodo.title.trim() !== '' && this.newTodo.description.trim()) {
                    api.createTodo( this.newTodo ).then(response => {
                        this.todos.push(response.data);
                        this.newTodo = {
                            title: '',
                            description: '',
                        };
                    })
                }
            },

            getTodos() {
                api.getTodos().then((response) => {
                    this.todos = response.data.map(todo => ({
                        ...todo,
                        isEditing: false,
                        editingText: '',
                    }))
                })
            },

            editTodo(todo) {
                todo.isEditing = true;
                todo.editingText = todo.title;
            },

            updateTodo(todo) {
                todo.isEditing = false;
                api.updateTode(todo).then(response => {
                    console.log(response);
                })
                    
            },

            removeTodo(todo) {
                api.removeTodo(todo).then(() => {
                    this.todos = this.todos.filter(t => t.id !== todo.id);
                })
            }
        },

        created() {
            this.getTodos()
        },
    };
</script>