<template>
    <h2>Add a ToDo</h2>
    <button @click="logout">Logout</button>
    <div>
        
        <form @submit.prevent="addTodo">

            <input class="title-input" v-model="newTodo.title" type="text" placeholder="New Todo Title" /><br />
            <br />
            <textarea class="description-input" v-model="newTodo.description" placeholder="New Todo Description"></textarea><br />
            <label for="image">Select image:</label>
            <input id="image" type="file" @change="onFileChange" />
            <br />
            <button type="submit">Add Todo</button>
        </form>
        <table>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Description</th>
                    <th class="completed-column">Completed</th>
                    <th class="edit-column">Edit</th>
                    <th>Images</th>

                </tr>
            </thead>
            <tbody>
                <tr v-for="todo in todos" :key="todo.id">

                    <td>
                        {{ todo.isEditing ? '' : todo.title }}
                        <input v-if="todo.isEditing" v-model="todo.title" type="text" @blur="updateTodo(todo)" />
                    </td>
                    <td>
                        {{ todo.isEditing ? '' : todo.description }}
                        <input v-if="todo.isEditing" v-model="todo.description" type="text" @blur="updateTodo(todo)" />
                    </td>
                    <td>
                        <input type="checkbox" @change="removeTodo(todo)" />
                    </td>
                    <td>
                        <button @click="editTodo(todo)">{{todo.isEditing ? 'Save' : 'Edit'}}</button>
                    </td>
                    <td>
                        <img v-if="!todo.isEditing" :src="todo.imageUrl" class="todo-image" />
                        <input v-if="todo.isEditing" type="file" @change="onEditFileChange(todo, $event)"/>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>
</template>

<script>
    import { ref, reactive, onMounted } from 'vue';
    import { useRouter } from 'vue-router';
    import api from '../api';

    export default {
        setup() {
            const todos = ref([]);
            const newTodo = reactive({
                title: '',
                description: '',
                file: null
                
            });
            const router = useRouter();

            const addTodo = () => {

                const formData = new FormData();
                formData.append('title', newTodo.title);
                formData.append('description', newTodo.description);
                if (newTodo.file) {
                    formData.append('image', newTodo.file);
                }

                api.createTodo(formData).then((response) => {
                    todos.value.push(response.data);
                    newTodo.title = '';
                    newTodo.description = '';
                    newTodo.file = null;
                });
            };

            const onFileChange = (e) => {
                console.log(e.target.files[0], "An image was uploaded");

                newTodo.file = e.target.files[0];
            }

            const getTodos = () => {
                api.getTodos().then((response) => {
                    todos.value = response.data.map((todo) => ({
                        ...todo,
                        isEditing: false,
                        editingText: todo.title,
                        imageUrl: todo.imageUrl
                    }));
                });
            };

            const editTodo = (todo) => {
                todo.isEditing = true;
                todo.file = null;
            };


            const onEditFileChange = (todo, e) => {
                //console.log(todo, "An image was changed");
                //console.log(e.target.files[0], "An image was changed for event");
                //todo.file = e.target.files[0];

                let reader = new FileReader();
                reader.onload = (e) => {
                    todo.imageUrl = e.target.result;
                    updateTodo(todo)
                };
                reader.readAsDataURL(e.target.files[0]);
            }

            const updateTodo = (todo) => {
                //console.log(todo);
                //console.log(todo);


                let formData = new FormData();
                formData.append('id', todo.id);
                //console.log(todo.id, " Is your id");
                formData.append('title', todo.title);
                //console.log(todo.title);
                formData.append('description', todo.description);
                //console.log(todo.description);

                formData.append('image', todo.imageUrl);
                console.log(todo.imageUrl);
                

                api.updateTodo(formData).then((response) => {
                    const index = todos.value.findIndex((t) => t.id === response.data.id);
                    if (todos.value[index]) {
                        todos.value.splice(index, 1, response.data);
                        todos.value[index].isEditing = false;
                        todos.value[index].editingText = '';
                    }
                    todo.imageUrl = response.data.imageUrl;
                });
            };





            const removeTodo = (todo) => {
                api.removeTodo(todo).then(() => {
                    todos.value = todos.value.filter((t) => t.id !== todo.id);
                });
            };

            const logout = () => {
                console.log("Token being removed is:", localStorage.getItem('token'));
                localStorage.removeItem('token');
                router.push('/login');
            }

            onMounted(getTodos);

            return {
                todos,
                newTodo,
                addTodo,
                onFileChange,
                getTodos,
                editTodo,
                updateTodo,
                removeTodo,
                logout,
                onEditFileChange,
            };
        },
    };

</script>
<style scoped>
    th {
        padding: 40px;
    }

    td {
        text-align: center;
    }

    .description-input {
        width: 300px;
        height: 50px;
        padding: 10px;
    }

    .description-input {
        resize: none; 
        overflow: hidden; 
        min-height: 50px; 
        max-height: 200px; 
    }

    .title-input {
        width: 315px;
        height: 20px;
        
    }

    tbody tr:nth-child(2n) {
        background-color: white;
    }
    
    tbody tr:nth-child(2n+1) {
        background-color: lightgrey;
    }

    .completed-column {
        width: 20px;
    }

    .todo-image {
        width: 100px;
        height: 100px;
    }



    



</style>
