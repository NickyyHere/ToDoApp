<script setup lang="ts">
import type { MessageType } from '../types/types';

defineProps<{
    message: string
    type: MessageType
}>()
</script>
<template>
    <div class="absolute notification bg-surface padding p-md border border-thicker">
        <p class="font-xxl" :class="type">{{ message }}</p>
    </div>
</template>
<style lang="css" scoped>
    .notification {
        right: 0;
        min-width: 10%;
        top: 10%;
        z-index: 5;
    }
</style>