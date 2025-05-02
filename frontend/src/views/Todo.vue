<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
import Column from '../components/Column.vue'
import { filterDataByStatus, redirect } from '../functions/utils'
import type { ProjectDTO, TaskDTO } from '../types/ItemData'
import { Type } from '../types/types'
import { fetchProjects, fetchTasks } from '../functions/communication'

const fetchType = ref<Type.task | Type.project>(Type.task)

const projectData = ref<ProjectDTO[]>([])
const taskData = ref<TaskDTO[]>([])

const newItems = ref<TaskDTO[] | ProjectDTO[]>([])
const inProgressItems = ref<TaskDTO[] | ProjectDTO[]>([])
const finishedItems = ref<TaskDTO[] | ProjectDTO[]>([])


const switchFetchType = () => {
    fetchType.value = fetchType.value == Type.task ? Type.project : Type.task
}

onMounted(async() => {
    taskData.value = await fetchTasks()
    projectData.value = await fetchProjects()
    watch(fetchType, async(newType) => {
        let data
        switch(newType) {
            case Type.project:
                data = projectData.value
                break
            case Type.task:
                data = taskData.value
                break
        }
        newItems.value = await filterDataByStatus(data, 0)
        inProgressItems.value = await filterDataByStatus(data, 1)
        finishedItems.value = await filterDataByStatus(data, 2)
    }, {immediate: true})
})
</script>
<template>
    <div class="text-center">
        <span class="font-xxl text-bold select-none inline-block margin-right m-md type" @click="switchFetchType">{{ fetchType.toUpperCase() }} <i class="fa-solid fa-rotate font-xxl"></i></span>
        <span class="font-xxl text-bold select-none inline-block add-button" @click="redirect('/add-new')">ADD NEW <i class="fa-solid fa-plus font-xxl"></i></span>
    </div>
    <div class="padding p-lg flex direction-row gap-sm">
        <Column title="NEW" :data="newItems"></Column>
        <Column title="IN PROGRESS" :data="inProgressItems"></Column>
        <Column title="FINISHED" :data="finishedItems"></Column>
    </div>
</template>
<style lang="css" scoped>
    .type, .add-button {
        cursor: pointer;
        transition: .3s;
    }
    .type:hover, .add-button:hover {
        color: var(--primary-color)
    }
</style>