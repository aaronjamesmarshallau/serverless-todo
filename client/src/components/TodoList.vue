<template>
  <ul class="todo-list">
    <simple-panel>
      <todo-list-item v-for="(item, index) in items" v-bind:key="index" :item="item" />
    </simple-panel>
  </ul>
</template>

<script>
import TodoListItem from '@/components/TodoListItem';
import SimplePanel from '@/components/SimplePanel';

export default {
  name: 'todo-list',
  components: {
    TodoListItem,
    SimplePanel,
  },
  props: {
    items: Array,
  },
};
</script>

<style scoped>
.todo-list {
  padding: 0;
}
</style>
