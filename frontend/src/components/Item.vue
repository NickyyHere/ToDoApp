<script setup lang="ts">
import { redirect, isTaskData } from '../functions/utils';
import type { TaskDTO, ProjectDTO } from '../types/ItemData';
    const props = defineProps<{
        itemData: TaskDTO | ProjectDTO
    }>()
    const finishDate = props.itemData.finishDate == null ? "ONGOING" : new Date(props.itemData.finishDate).toLocaleDateString()
</script>
<template>
    <div class="bg-surface border rounded itemWrapper">
        <div class="item" @click="redirect({url: '/details', props: props.itemData})">
            <p class="text-center font-xxl">{{ itemData.name }}</p>
            <p class="text-center" v-if="isTaskData(itemData)">{{ itemData.projectName }}</p>
            <hr class="margin m-xs">
            <p class="font-sm">{{ itemData.description }}</p>
            <p class="text-center">{{ new Date(props.itemData.startDate).toLocaleDateString() }} - {{ finishDate }}</p>
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