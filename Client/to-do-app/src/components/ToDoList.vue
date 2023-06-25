<template>
    <h2>Add a ToDo</h2>
    <div>
        <form @submit.prevent="addTodo">
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
                    
                </tr>
            </thead>
            <tbody>
                <tr v-for="todo in todos" :key="todo.id">

                    <td v-if="!todo.isEditing">
                        {{ todo.title }}
                        <button @click="editTodo(todo)">Edit</button>
                    </td>
                    <td v-else>
                        <input v-model="todo.title" type="text" @blur="updateTodo(todo)" />
                    </td>

                    <td v-if="!todo.isEditing">
                        {{ todo.description }}
                    </td>
                    <td v-else>
                        <input v-model="todo.description" type="text" @blur="updateTodo(todo)" />
                    </td>


                    <td>
                        <input type="checkbox" @change="removeTodo(todo)" />
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
                
                api.createTodo( this.newTodo ).then(response => {
                    this.todos.push(response.data);
                    this.newTodo = {
                        title: '',
                        description: '',
                    };
                })
                
            },

            getTodos() {
                api.getTodos().then((response) => {
                    this.todos = response.data.map(todo => ({
                        ...todo,
                        isEditing: false,
                        editingText: todo.title,
                    }))
                })
            },

            editTodo(todo) {
                todo.isEditing = true;
            },

            updateTodo(todo) {
                api.updateTodo(todo).then((response) => {
                    const index = this.todos.findIndex((t) => t.id === response.data.id);
                    if (this.todos[index]) {
                        
                        this.$set(this.todos, index, response.data);
                        this.$set(this.todos[index], 'isEditing', false);
                        
                        this.todos.forEach((_, i) => {
                            this.$set(this.todos[i], 'editingText', '');
                        });
                    }
                    
                    
                });
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