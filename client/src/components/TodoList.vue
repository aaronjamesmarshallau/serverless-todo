<template>
  <ul class="todo-list">
    <todo-list-item v-for="(item, index) in items" v-bind:key="index" :item="item" />
  </ul>
</template>

<script>
import TodoListItem from '@/components/TodoListItem';

export default {
  name: 'todo-list',
  components: {
    TodoListItem,
  },
  props: {
    items: Array,
  },
};
</script>

<style scoped>
.todo-list {
  padding: 0;
  box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
}
</style>
