<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
import { useRoute } from 'vue-router'
import Column from '../components/Column.vue'
import { fetchItems } from '../functions/communication'
import { filterDataByStatus, redirect } from '../functions/utils'
import type { ProjectData, TaskData } from '../types/ItemData'

const route = useRoute()
const raw = route.query.data as string
const data = raw ? JSON.parse(raw) as {notification: string} : null

const notification = ref<string | null>('')

const fetchType = ref<"tasks" | "projects">("tasks")
const newItems = ref<TaskData[] | ProjectData[]>([])
const inProgressItems = ref<TaskData[] | ProjectData[]>([])
const finishedItems = ref<TaskData[] | ProjectData[]>([])

watch(fetchType, async(newType) => {
    const data = await fetchItems(newType)
    newItems.value = await filterDataByStatus(data as ProjectData[], 0)
    inProgressItems.value = await filterDataByStatus(data as ProjectData[], 1)
    finishedItems.value = await filterDataByStatus(data as ProjectData[], 2)
}, {immediate: true})

const switchFetchType = () => {
    fetchType.value = fetchType.value == "tasks" ? "projects" : "tasks"
}

onMounted(() => {
    if(data != null) {
        notification.value = data.notification
        setTimeout(() => {notification.value = null}, 3000)
    }
})
</script>
<template>
    <div class="notification padding p-md border border-thick bg-primary" v-if="notification">
        <h3>{{ notification }}</h3>
    </div>
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