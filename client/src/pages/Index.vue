<template>
  <div class="index">
    <div class="todo-panel">
      <h1>Todo List</h1>
      <todo-list :items="items" />
    </div>
  </div>
</template>

<script>
import { TodoApi } from '@/apis';
import TodoList from '@/components/TodoList';

export default {
  name: 'index',
  components: {
    TodoList,
  },
  data() {
    return {
      items: [],
    };
  },
  async mounted() {
    this.items = await TodoApi.get()
      .then(response => response.data)
      .catch(() => console.log('error'));
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
