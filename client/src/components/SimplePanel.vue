<template>
  <div class="simple-panel">
    <slot></slot>
  </div>
</template>

<script>
export default {
  name: 'simple-panel',
};
</script>

<style scoped>
.simple-panel {
  box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
}
</style>
