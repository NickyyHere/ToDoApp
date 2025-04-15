<script setup lang="ts">
import { useRoute } from 'vue-router'

const route = useRoute()
const raw = route.query.data as string
const data = raw ? JSON.parse(raw) : null

</script>
<template>
    {{ data }}
</template>
<style lang="css" scoped>
</style>