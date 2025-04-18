<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { fetchItems, updateItem, deleteItem } from '../functions/communication'
import type { ProjectData, TaskData } from '../types/ItemData'
import { formToTypeDataUpdate, isTaskData, labelAsCheckbox, redirect, statusToText } from '../functions/utils'

const route = useRoute()
const raw = route.query.data as string
const data = raw ? JSON.parse(raw) as ProjectData | TaskData : null

const isEditing = ref<boolean>(false)
const checkedTechnologies = ref<string[]>([])

const projects =  ref<ProjectData[]>([])
const technologies = ref<TaskData[]>([])

const startDate = new Date((data?.startDate) as string)
const finishDate = data?.finishDate == null ? "ONGOING" : new Date(data.finishDate).toLocaleDateString()

const validation = ref<string | null>(null)

const confimation = ref<string>('')
const confirmationResult = ref<string | null>(null)
const viewConfirmation = ref<boolean>(false)

const notification = ref<string | null>('')

const formData = ref({
    name: (data as TaskData | ProjectData).name,
    description: (data as TaskData | ProjectData).description,
    projectName: (data as TaskData).projectName,
    technologies: new Array<string>
})

const sendForm = async () => {
    formData.value.technologies = checkedTechnologies.value
    if(!validateForm()) {
        return
    }
    const type = isTaskData(data) ? "tasks" : "projects"
    const response = await updateItem(type, formToTypeDataUpdate(type, formData.value), data?.id as number)
    if(response == 200)
        redirect({url: '/', props: {notification: "Item updated successfuly"}})
    else {
        notification.value = "Updating item failed!"
        viewConfirmation.value = false
        setTimeout(() => {notification.value = null}, 3000)
    }
}

function validateForm(): boolean  {
    if(formData.value.name.length == 0) {
        validation.value = "Name can't be empty"
        return false
    }
    if (formData.value.description.length == 0) {
        validation.value = "Description can't be empty"
        return false
    }
    if(isTaskData(data)) {
        if(formData.value.technologies.length == 0) {
            validation.value = "Select technologies"
            return false
        }
    }
    return true
}


async function delItem() {
    if(confimation.value !== data?.name + '#' + data?.id) {
        confirmationResult.value = "Invalid input"
        return
    }
    let response: number | undefined
    if(isTaskData(data))
        response = await deleteItem("tasks", (data as TaskData).id)
    else
        response = await deleteItem("projects", (data as ProjectData).id)

    if(response == 200)
        redirect({url: '/', props: {notification: "Item deleted successfuly"}})
    else {
        notification.value = "Deleting item failed!"
        viewConfirmation.value = false
        setTimeout(() => {notification.value = null}, 3000)
    }
}

onMounted(async () => {
    projects.value = (await fetchItems("projects")) as ProjectData[]
    technologies.value = (await fetchItems("technologies")) as TaskData[]
})
</script>
<template>
    <div class="notification padding p-md border border-thick bg-primary" v-if="notification">
        <h3 class="font-error">{{ notification }}</h3>
    </div>
    <div v-if="viewConfirmation" class="cover bg-primary padding p-lg border border-thicker">
        <div class="close font-xxl" @click="viewConfirmation=false">X</div>
        <div class="cover-item absolute-center bg-primary border border-thicker padding p-lg flex justify-center direction-col text-center">
            <h3>CONFIRM DELETING ITEM</h3>
            <hr>
            <h3>Type name and id of item to delete</h3>
            <p class="font-error font-xl" v-if="confirmationResult">{{ confirmationResult }}</p>
            <div>
                <input type="text" :placeholder="'NAME#ID'" class="font-xxl padding p-xs" v-model="confimation">
            </div>
            <button class="padding p-xs margin m-md" @click="delItem()">CONFIRM</button>
        </div>
    </div>
    <div class="text-center">
        <div v-if="isEditing">
            <h2>
                {{ isTaskData(data) ? "TASK" : "PROJECT" }} #{{ data?.id }}
            </h2>
            <p v-if="validation" class="font-error margin-top m-lg font-xl">{{ validation }}</p>
        </div>
        <form @submit.prevent="sendForm">
            <div v-if="isEditing">
                <div>
                    <label for="name" class="font-xxl">NAME</label><br>
                    <input name="name" class="padding p-xs font-xl text-center" v-model="formData.name">
                </div>
            </div>
            <div v-else>
                <h2>{{ data?.name }}<span class="absolute text-color-muted">#{{ data?.id }}</span></h2>
            </div>
            <div v-if="isEditing">
                <div v-if="isTaskData(data)">
                    <label for="projectName" class="font-xxl">PROJECT</label><br>
                    <select name="projectName" class="padding p-xs font-xl text-center" v-model="formData.projectName">
                        <option v-for="project in projects" :value="project.name">{{ project.name }}</option>
                    </select>
                </div>
            </div>
            <div v-else>
                <p v-if="isTaskData(data)" class="font-xl">{{ data?.projectName }}</p>
            </div>
            <div v-if="isEditing">
                <div>
                    <label for="description" class="font-xxl">DESCRIPTION</label><br>
                    <textarea name="description" class="padding p-xs font-xl" v-model="formData.description"></textarea>
                </div>
            </div>
            <div v-else class="margin-top m-md">
                <hr>
                <h3>DESCRIPTION</h3>
                <p class="text-left padding p-lg">{{ data?.description }}</p>
                <hr>
            </div>
            <div v-if="isEditing">
                <div v-if="isTaskData(data)">
                    <p class="font-xxl">TECHNOLOGIES</p>
                    <div v-if="isTaskData(data)" class="flex justify-center margin-top m-xs select-none gap-sm">
                        <div v-for="technology in technologies" >
                            <label :for="technology.name" class="border rounded padding p-xs font-xl checkbox" @click="labelAsCheckbox($event.target as HTMLElement, checkedTechnologies)">{{ technology.name }}</label>
                            <input type="checkbox" :value="technology.name" class="hidden">
                        </div>
                    </div>
                </div>
            </div>
            <div v-else>
                <div v-if="isTaskData(data)" class="flex justify-center margin-top m-md gap-sm">
                    <div v-for="technology in data?.technologies" class="border rounded padding p-xs font-xl">{{ technology }}</div>
                </div>
            </div>
            <div v-if="isEditing" class="margin-top m-xl flex justify-center gap-sm">
                <button class="padding p-xs font-xxl">SAVE</button>
                <button class="padding p-xs font-xxl" @click="isEditing=false">CANCEL</button>
            </div>
            <div v-else >
                <p class="margin-top m-md font-xl">{{ statusToText((data as TaskData | ProjectData).status) }}</p>
                <p class="margin-top m-md font-xl">{{ startDate.toLocaleDateString() }} - {{ finishDate }}</p>
                <div class="margin-top m-xl flex justify-center gap-sm">
                    <button class="padding p-xs font-xxl" @click="isEditing=true">EDIT</button>
                    <button class="padding p-xs font-xxl" @click="viewConfirmation=true">DELETE</button>
                </div>
            </div>
        </form>
    </div>
</template>
<style lang="css" scoped>
input, textarea {
    min-width: 15vw;
    max-width: 15vw;
}
textarea {
    min-height: 20vh;
}
</style>