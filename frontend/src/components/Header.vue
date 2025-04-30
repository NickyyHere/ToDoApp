<script setup lang="ts">
import { redirect } from '../functions/utils';

</script>
<template>
    <div class="bg-surface padding p-sm font-res-xs flex align-start gap-lg header">
        <div class="link" @click="redirect('/')"><i class="fa-solid fa-house"></i> ToDoApp</div>
        <div class="select-none separator">|</div>
        <div class="link" @click="redirect('/statistics')"><i class="fa-solid fa-chart-simple"></i> Statistics</div>
        <div class="select-none separator">|</div>
        <div class="link" @click="redirect('/data')"><i class="fa-solid fa-database"></i> Data</div>
    </div>
</template>
<style lang="css" scoped>
    .header {
        border-bottom: 2px solid var(--border-color);
    }
    .separator {
        color: var(--border-color);
    }
    .link {
        cursor: pointer;
        transition: .3s;
    }
    .link:hover {
        color: var(--primary-color)
    }
</style>