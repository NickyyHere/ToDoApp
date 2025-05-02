<script lang="ts" setup>
import { onMounted, ref } from 'vue'
import {  labelAsCheckbox, processResponseStatus, redirect } from '../functions/utils'
import { fetchProjects, fetchTechnologies } from '../functions/communication'
import type { ProjectDTO, TechnologyDTO } from '../types/ItemData'
import { Action, MessageType, Type } from '../types/types'
import { sendForm } from '../functions/form_utils'
import emitter from '../types/emitter'

const itemType = ref<Type.task | Type.project | Type.technology>(Type.task)

const projects = ref<ProjectDTO[]>([])
const technologies = ref<TechnologyDTO[]>([])

const formData = ref({
    name: '',
    description: '',
    projectId: -1,
    technologyNames: new Array<string>
})

const handleChangeType = () => {
    const form = document.querySelector('form') as HTMLFormElement
    form.reset()
    formData.value.name = ''
    formData.value.description = ''
    formData.value.projectId = -1
    formData.value.technologyNames = []
}
const handleAdd = async () => {
    let status: number
    const onSuccess = () => {
        redirect("/")
        emitter.emit("showNotification", {messageType: MessageType.success, message: "Successfuly added new item"})
    }
    const checkboxes_wrapper = document.querySelector('.checkbox')?.closest('div')?.parentElement
    const checkboxes = checkboxes_wrapper?.querySelectorAll('input[type="checkbox"]:checked') || []
    formData.value.technologyNames = Array.from(checkboxes).map(cb => (cb as HTMLInputElement).getAttribute('value') || '')
    status = await sendForm(Action.add, itemType.value, formData.value)
    processResponseStatus(status, onSuccess)
}
onMounted(async () => {
    projects.value = await fetchProjects()
    technologies.value = await fetchTechnologies()
})

</script>
<template>
    <!-- Main -->
    <div class="flex direction-col text-center">
        <h1>ADD NEW ITEM</h1>
        <hr>
        <!-- Selecting type of item to add -->
        <div>
            <label for="type" class="text-center block font-xxl margin-top m-md">TYPE</label><br>
            <select v-model="itemType" name="type" @change="handleChangeType" class="font-xl padding p-xs input">
                <option :value="Type.task">Task</option>
                <option :value="Type.project">Project</option>
                <option :value="Type.technology">Technology</option>
            </select>
        </div>
        <!-- On submit call sendForm() -->
        <form @submit.prevent="handleAdd">
            <!-- Input for "name" -->
            <div>
                <label for="name" class="text-center block font-xxl margin-top m-md">NAME</label><br>
                <input type="text" name="name" v-model="formData.name" class="font-xl padding p-xs input" required>
            </div>
            <!-- Input for "description" -->
            <div v-if="itemType === 'tasks' || itemType === 'projects'">
                <label for="description" class="text-center block font-xxl margin-top m-md">DESCRIPTION</label><br>
                <textarea name="description" v-model="formData.description" class="font-md padding p-xs description input" required></textarea>
            </div>
            <!-- Select for project -->
            <div v-if="itemType === 'tasks'">
                <label for="project" class="text-center block font-xxl margin-top m-md">PROJECT</label>
                <select name="project" v-model="formData.projectId" class="font-xl padding p-xs input">
                    <option v-for="project in projects" :value="project.id"> {{ project.name }} </option>
                </select>
            </div>
            <!-- Checkboxes for tasks -->
            <div v-if="itemType === 'tasks'" class="select-none margin-top margin-bottom m-md">
                <p class="text-center block font-xxl margin-top m-md margin-bottom">TECHNOLOGIES</p>
                <div class="flex justify-center align-start wrap gap-md width-50 margin-left margin-right m-auto">
                    <div v-for="technology in technologies">
                        <label :for="technology.name" class="checkbox font-xxl padding p-xs" @click="labelAsCheckbox($event.target as HTMLElement)">{{ technology.name }}</label>
                        <input type="checkbox" :name="technology.name" :value="technology.name" class="hidden">
                    </div>
                </div>
                
            </div>
            <button type="submit" class="font-xxl padding p-xs rounded margin-top m-md">Submit</button>
        </form>
    </div>
</template>
<style lang="css" scoped>
    .input {
        width: 15vw;
        border: 1px solid var(--border-color);
    }
    textarea.description {
        max-width: 15vw;
        min-width: 15vw;
        min-height: 20vh;
    }
</style>