<script setup lang="ts">
  import Header from './components/Header.vue'
</script>

<template>
  <div>
    <Header class="margin-bottom m-lg"></Header>
    <div class="margin-left margin-right m-lg">
        <RouterView />
    </div>
  </div>
</template>

<style lang="css" scoped>
</style>