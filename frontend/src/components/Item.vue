<script setup lang="ts">
import { redirect, isTaskData, truncateText } from '../functions/utils';
import type { TaskDTO, ProjectDTO } from '../types/ItemData';
import { Type } from '../types/types';
    const props = defineProps<{
        itemData: TaskDTO | ProjectDTO
    }>()
    const startDate = props.itemData.startDate == null ? "NEW" : new Date(props.itemData.startDate).toLocaleDateString()
    const finishDate = props.itemData.finishDate == null ? "ONGOING" : new Date(props.itemData.finishDate).toLocaleDateString()
</script>
<template>
    <div class="bg-surface border rounded itemWrapper">
        <div class="item" @click="redirect({url: `/details/${isTaskData(props.itemData) ? Type.task : Type.project}/${props.itemData.id}`})">
            <p class="text-center font-xxl">{{ itemData.name }}</p>
            <p class="text-center" v-if="isTaskData(props.itemData)">{{ props.itemData.projectName }}</p>
            <hr class="margin m-xs">
            <p class="font-sm">{{ truncateText(itemData.description, 50) }}</p>
            <p class="text-center">{{ startDate }}<span v-if="props.itemData.startDate"> - {{ finishDate }}</span></p>
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