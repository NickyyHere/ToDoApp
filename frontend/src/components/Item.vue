<script setup lang="ts">
import { redirect } from '../functions/utils';
import type { ProjectData, TaskData } from '../types/ItemData';
    const props = defineProps<{
        itemData: TaskData | ProjectData
    }>()
    const prop = props.itemData
</script>
<template>
    <div class="bg-surface border rounded itemWrapper">
        <div class="item" @click="redirect({url: '/details', props: prop})">
            <p class="text-center">{{ itemData.name }}</p>
            <p class="font-sm">{{ itemData.description }}</p>
        </div>
    </div>
</template>
<style scoped lang="css">
    .itemWrapper {
        overflow: hidden;
        cursor: pointer;
        transition: .5s;
    }
    .itemWrapper:hover {
        border-color: var(--primary-color);
    }
    .item {
        transition: .5s;
    }
    .item:hover {
        transform: scale(1.5);
    }
</style>