<script lang="ts" setup>
import { onMounted, ref } from 'vue'
import { formToTypeData, labelAsCheckbox, redirect } from '../functions/utils'
import { addNewItem, fetchItems } from '../functions/communication'
import type { TechnologyData } from '../types/ItemData'

const itemType = ref<"tasks" | "projects" | "technologies">("tasks")
const checkedTechnologies = ref<string[]>([])

const projects = ref<any[]>([])
const technologies = ref<TechnologyData[]>([])

const validation = ref<string | null>(null)

const notification = ref<string | null>(null)

const formData = ref({
    name: '',
    description: '',
    project: -1,
    technologies: new Array<string>
})

const handleChangeType = () => {
    const form = document.querySelector('form') as HTMLFormElement
    form.reset()
    checkedTechnologies.value = []
    formData.value.name = ''
    formData.value.description = ''
    formData.value.project = -1
    formData.value.technologies = []
}

const sendForm = async () => {
    formData.value.technologies = checkedTechnologies.value
    if(!validateForm()) {
        return
    }
    let response: number | undefined
    if(formData.value.project == -1) {
        response = await addNewItem(itemType.value, formToTypeData(itemType.value, formData.value))
    } else {
        response = await addNewItem(itemType.value, formToTypeData(itemType.value, formData.value), formData.value.project)
    }
    if(response == 200)
        redirect({url: '/', props: {notification: "Item added"}})
    else {
        notification.value = "Updating item failed!"
        setTimeout(() => {notification.value = null}, 3000)
    }
}

function validateForm(): boolean  {
    if(formData.value.name.length == 0) {
        validation.value = "Name can't be empty"
        return false
    }
    if((itemType.value == "tasks" || itemType.value == "projects") && formData.value.description.length == 0) {
        validation.value = "Description can't be empty"
        return false
    }
    if(itemType.value == "tasks") {
        if(formData.value.project == -1) {
            validation.value = "Select a project"
            return false
        }
        if(formData.value.technologies.length == 0) {
            validation.value = "Select technologies"
            return false
        }
    }
    return true
}

onMounted(async () => {
    projects.value = await fetchItems("projects")
    technologies.value = await fetchItems("technologies")
})

</script>
<template>
    <div class="notification padding p-md border border-thick bg-primary" v-if="notification">
    <h3 class="font-error">{{ notification }}</h3>
    </div>
    <div class="flex direction-col text-center">
        <h1>ADD NEW ITEM</h1>
        <hr>
        <p v-if="validation" class="font-error margin-top m-lg font-xl">{{ validation }}</p>
        <div>
            <label for="type" class="text-center block font-xxl margin-top m-md">TYPE</label><br>
            <select v-model="itemType" name="type" @change="handleChangeType" class="font-xl padding p-xs input">
                <option value="tasks">Task</option>
                <option value="projects">Project</option>
                <option value="technologies">Technology</option>
            </select>
        </div>
        <form @submit.prevent="sendForm">
            <div>
                <label for="name" class="text-center block font-xxl margin-top m-md">NAME</label><br>
                <input type="text" name="name" v-model="formData.name" class="font-xl padding p-xs input" required>
            </div>
            <div v-if="itemType === 'tasks' || itemType === 'projects'">
                <label for="description" class="text-center block font-xxl margin-top m-md">DESCRIPTION</label><br>
                <textarea name="description" v-model="formData.description" class="font-md padding p-xs description input" required></textarea>
            </div>
            <div v-if="itemType === 'tasks'">
                <label for="project" class="text-center block font-xxl margin-top m-md">PROJECT</label>
                <select name="project" v-model="formData.project" class="font-xl padding p-xs input">
                    <option v-for="project in projects" :value="project.id"> {{ project.name }} </option>
                </select>
            </div>
            <div v-if="itemType === 'tasks'" class="select-none margin-top margin-bottom m-md">
                <div class="inline-block" v-for="technology in technologies">
                    <label :for="technology.name" class="checkbox font-xxl padding p-xs margin m-xs" @click="labelAsCheckbox($event.target as HTMLElement, checkedTechnologies)">{{ technology.name }}</label>
                    <input type="checkbox" :name="technology.name" :value="technology.name" class="hidden">
                </div>
            </div>
            <button type="submit" class="font-xxl padding p-xs rounded">Submit</button>
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