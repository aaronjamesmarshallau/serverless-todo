<template>
  <li class="todo-list-item">
    <div class="content-anchor">
      <div class="check-box">
        <input type="checkbox" v-on:click="onCheckToggled" />
      </div>
      <div class="item-summary">
        <s v-if="checked">
          {{ item.summary }}
        </s>
        <span v-else>
          {{ item.summary }}
        </span>
      </div>
    </div>
  </li>
</template>

<script>
export default {
  name: 'todo-list-item',
  props: {
    item: Object,
  },
  methods: {
    onCheckToggled(evt) {
      this.item.isComplete = evt.target.checked;
    },
  },
  computed: {
    checked() {
      return this.item.isComplete;
    },
  },
};
</script>

<style scoped>
.check-box {
  height: 1em;
  width: 1em;
  margin-right: 5px;
}

.content-anchor > div {
  display: inline-block;
}

.todo-list-item:not(:last-of-type) {
  border-bottom: 1px solid #ddd;
}

.todo-list-item {
  position: relative;
  list-style: none;
  padding: 10px;
  z-index: 1;
  text-align: left;
}
</style>
