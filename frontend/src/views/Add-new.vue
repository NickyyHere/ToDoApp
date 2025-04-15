<script lang="ts" setup>
import { onMounted, ref } from 'vue'
import { formToTypeData } from '../functions/utils'
import { addNewItem, fetchItems } from '../functions/communication'
import type { TechnologyData } from '../types/ItemData'

const itemType = ref<"tasks" | "projects" | "technologies">("tasks")
const checkedTechnologies = ref<TechnologyData[]>([])
const projects = ref<any[]>([])
const technologies = ref<TechnologyData[]>([])

const formData = ref({
    name: '',
    description: '',
    project: -1,
    technologies: new Array<TechnologyData>
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

const labelAsCheckbox = (event: Event) => {
    const target = event.target as HTMLElement
    const checkbox = target.closest('div')?.querySelector('input[type="checkbox"]') as HTMLInputElement
    checkbox.checked = !checkbox.checked
    target.classList.toggle('checked')
    const technology: TechnologyData = {
        id: parseInt(checkbox.value),
        name: checkbox.name
    }
    let index: number | null = null
    if(checkedTechnologies.value.length == 0) {
        checkedTechnologies.value.push(technology)
    } else {
        for(let i = 0; i < checkedTechnologies.value.length; i++) {
            if(checkedTechnologies.value[i].id == technology.id) {
                index = i
                break
            }
        }

        if(index == null) {
            checkedTechnologies.value.push(technology)
        } else {
            checkedTechnologies.value.splice(index, 1)
        }
    }
}

const sendForm = async () => {
    formData.value.technologies = checkedTechnologies.value
    if(formData.value.project == -1) {
        await addNewItem(itemType.value, formToTypeData(itemType.value, formData.value))
    } else {
        await addNewItem(itemType.value, formToTypeData(itemType.value, formData.value), formData.value.project)
    }
}

onMounted(async () => {
    projects.value = await fetchItems("projects")
    technologies.value = await fetchItems("technologies")
})

</script>
<template>
    <div>
        <label for="type">Type:</label>
        <select v-model="itemType" name="type" @change="handleChangeType">
            <option value="tasks">Task</option>
            <option value="projects">Project</option>
            <option value="technologies">Technology</option>
        </select>
    </div>
    <form @submit.prevent="sendForm" class="form">
        <div>
            <label for="name">Name:</label>
            <input type="text" name="name" v-model="formData.name">
        </div>
        <div v-if="itemType === 'tasks' || itemType === 'projects'">
            <label for="description">Description</label>
            <textarea name="description" v-model="formData.description"></textarea>
        </div>
        <div v-if="itemType === 'tasks'">
            <label for="project">Project:</label>
            <select name="project" v-model="formData.project" >
                <option v-for="project in projects" :value="project.id"> {{ project.name }} </option>
            </select>
        </div>
        <div v-if="itemType === 'tasks'" class="select-none margin-top margin-bottom m-md">
            <div class="inline-block" v-for="technology in technologies">
                <label :for="technology.name" class="checkbox font-xxl padding p-xs margin m-xs" @click="labelAsCheckbox">{{ technology.name }}</label>
                <input type="checkbox" :name="technology.name" :value="technology.id" class="hidden">
            </div>
        </div>
        <button type="submit">Submit</button>
    </form>
</template>
<style lang="css" scoped>
</style>