<template>
  <div class="todo-input">
    <simple-panel>
      <input placeholder="What do you need to do?" ref="field" class="todo-input-el" type="text" v-model="todoSummary" v-on:keyup="onKeyUp" :disabled="disabled" />
    </simple-panel>
  </div>
</template>

<script>
import SimplePanel from '@/components/SimplePanel';

export default {
  name: 'todo-input',
  components: {
    SimplePanel,
  },
  props: {
    disabled: Boolean,
  },
  data() {
    return {
      todoSummary: '',
    };
  },
  methods: {
    clear() {
      this.todoSummary = '';
    },
    onKeyUp(evt) {
      if (evt.key !== 'Enter') {
        return false;
      }

      this.$emit('submit', this.todoSummary);
      return true;
    },
  },
};
</script>

<style scoped>
.todo-input-el {
  box-sizing: border-box;
  padding: 10px;
  border: none;
  width: 100%;
  font-size: inherit;
  font-family: inherit;
}

.todo-input-el:focus {
  outline: 0;
}
</style>
