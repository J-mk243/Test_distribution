<template>
    <div class="todo-list">
        <ul class="list-group">
            <li v-for="taskItem in tasksItems" :key="taskItem.id" class="list-group-item d-flex justify-content-between align-items-center">
                <div>{{ taskItem.title }}</div>
                <button class="btn btn-danger" @click="deleteItem(taskItem.id)">Delete</button>
            </li>
        </ul>
        <div class="input-group mb-3">
            <input v-model="newItem" @keyup.enter="addItem" placeholder="Enter the title of the new task" class="form-control" />
            <br />
            <button class="btn btn-primary" @click="addItem">Add task</button>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    data() {
        return {
           tasksItems : [],
            newItem: '',
            checkedItems: []
        };
    },
    methods: {
        async addItem() {
            try {
                const response = await axios.post('https://localhost:7276/api/TaskItems', { title: this.newItem });
                this.tasksItems.push(response.data);
                this.newItem = '';
            } catch (error) {
                console.error(error);
            }
        },
        async deleteItem(itemId) {
            try {
                await axios.delete(`https://localhost:7276/api/TaskItems/${itemId}`);
                this.tasksItems = this.tasksItems.filter(item => item.id !== itemId);
            } catch (error) {
                console.error(error);
            }
        },
    },
    async mounted() {
        try {
            const response = await axios.get('https://localhost:7276/api/TaskItems');
            this.tasksItems = response.data;
        } catch (error) {
            console.error(error);
        }
    }
}
</script>

<style scoped>
.list-group-item {
    gap: 0.5rem;
    width: 60%;
    margin: auto;
    margin-top: 5px;
}
.todo-list {
    width: 70%;
    margin: 0 auto;
}
.list-group {
    max-height: 70%;
    overflow-y: auto;
}
.input-group {
    margin: auto;
    width: 70%;
    margin-top: 12px;
}
</style>
