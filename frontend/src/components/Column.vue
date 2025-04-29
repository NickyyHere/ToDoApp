<script setup lang="ts">
import type { ProjectDTO, TaskDTO } from '../types/ItemData';
import Item from './Item.vue';

    defineProps<{
        title: string
        data: TaskDTO[] | ProjectDTO[]
    }>()
</script>
<template>
    <div class="bg-primary flex border border-thick direction-col width-100 wrapper">
        <div class="bg-primary padding-left padding-right p-sm flex direction-col flex-item container">
            <h3 class="text-center"> {{ title }} </h3>
            <hr>
            <div class="flex-item margin-bottom margin-top m-sm font-xxl items gap-xs">
                <Item v-for="item in data" :item-data="item"></Item>
            </div>
        </div>
    </div>
</template>
<style lang="css" scoped>
    .wrapper {
        height: 70vh;
        max-height: 70vh;
    }
    .container {
        min-height: 0;
    }
    .items {
        column-count: 2;
        column-gap: 1rem;
        overflow-y: scroll;
    }
    .items div {
        break-inside: avoid;
        margin-bottom: 1rem;
        padding: 1rem;
    }
    ::-webkit-scrollbar {
        width: 8px;
    }
    ::-webkit-scrollbar-track {
        width: 0.5rem;
        border-radius: 10px;
        background-color: var(--background-color);
    }
    ::-webkit-scrollbar-thumb {
        border-radius: 10px;
        background-color: var(--accent-color);
    }
</style>