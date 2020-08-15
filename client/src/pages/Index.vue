<template>
  <div class="index">
    <div class="todo-panel">
      <h1>Todo List</h1>
      <todo-input ref="todoInput" v-on:submit="onTodoCreate" :disabled="addingCreate" />
      <todo-list :items="items" />
    </div>
  </div>
</template>

<script>
import { TodoApi } from '@/apis';
import TodoList from '@/components/TodoList';
import TodoInput from '@/components/TodoInput';

export default {
  name: 'index',
  components: {
    TodoList,
    TodoInput,
  },
  data() {
    return {
      items: [],
      addingCreate: false,
    };
  },
  async mounted() {
    this.items = await TodoApi.get()
      .then(response => response.data)
      .catch(() => console.log('error'));
  },
  methods: {
    onTodoCreate(summary) {
      const newItem = { summary };

      this.addingCreate = true;

      TodoApi.create(newItem)
        .then(() => {
          this.addingCreate = false;
          this.$refs.todoInput.clear();
          this.items.unshift(newItem);
        })
        .catch(() => {
          this.addingCreate = false;
        });
    },
  },
};
</script>

<style scoped>
.index {
  text-align: center;
}

.todo-panel {
  max-width: 100%;
  width: 640px;
  margin: auto;
}
</style>
