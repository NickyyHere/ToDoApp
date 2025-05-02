<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { changeProjectStatus, changeTaskStatus, deleteProject, deleteTask, fetchProject, fetchProjects, fetchTask, fetchTechnologies } from '../functions/communication'
import type { ProjectDTO, TaskDTO, TechnologyDTO } from '../types/ItemData'
import { isTaskData, labelAsCheckbox, processResponseStatus, redirect, statusToText } from '../functions/utils'
import { sendForm } from '../functions/form_utils'
import { Action, MessageType, Type } from '../types/types'
import emitter from '../types/emitter'

const route = useRoute()
const type = route.params.type as Type
const id = parseInt(route.params.id as string)

const data = ref<TaskDTO | ProjectDTO>({} as TaskDTO)

const isEditing = ref<boolean>(false)

const projects =  ref<ProjectDTO[]>([])
const technologies = ref<TechnologyDTO[]>([])

const startDate = ref<string>('')
const finishDate = ref<string>('')

const confimation = ref<string>('')
const confirmationResult = ref<string | null>(null)
const viewConfirmation = ref<boolean>(false)

const blockStatusChange = ref<boolean>(false)

const onMissing = () => {
    emitter.emit("showNotification", {messageType: MessageType.error, message: "Coulnd't find resource"})
}

const formData = ref<{
    name: string
    description: string
    projectId: number
    technologyNames: string[]
}>({ // If this is not here it throws a lot of "formData may be undefined", this is filled in onMounted
    name: '',
    description: '',
    projectId: -1,
    technologyNames: []
})

const handleDelete = async () => {
    let status: number
    const onSuccess = () => {
        redirect("/")
        emitter.emit("showNotification", {messageType: MessageType.success, message: "Item deleted"})
    }
    const onError = () => {
        emitter.emit("showNotification", {messageType: MessageType.success, message: "Item deletion failed"})
    }
    switch(type) {
        case Type.project:
            status = await deleteProject(id)
            break
        case Type.task:
            status = await deleteTask(id)
            break
        default:
            status = 400
    }
    processResponseStatus(status, onSuccess, onError, onMissing)
}
const handleChangeStatus = async() => {
    let status: number
    const onSuccess = () => {
        blockStatusChange.value = true
        data.value.status += 1
        emitter.emit("showNotification", {messageType: MessageType.success, message: "Item status changed"})
    }
    const onError = () => {
        emitter.emit("showNotification", {messageType: MessageType.success, message: "Failed to change item status"})
    }
    switch(type) {
        case Type.project:
            status = await changeProjectStatus(id)
            break
        case Type.task:
            status = await changeTaskStatus(id)
            break
        default:
            status = 400
    }
    processResponseStatus(status, onSuccess, onError, onMissing)
}
const handleUpdate = async() => {
    let status: number
    const onSuccess = () => {
        redirect("/")
        emitter.emit("showNotification", {messageType: MessageType.success, message: "Item updated"})
    }
    const onError = () => {
        emitter.emit("showNotification", {messageType: MessageType.error, message: "Failed updating item"})
    }
    const checkboxes_wrapper = document.querySelector('.checkbox')?.closest('div')?.parentElement
    const checkboxes = checkboxes_wrapper?.querySelectorAll('input[type="checkbox"]:checked') || []
    formData.value.technologyNames = Array.from(checkboxes).map(cb => (cb as HTMLInputElement).getAttribute('value') || '')
    status = await sendForm(Action.update, type, formData.value, data.value?.id)
    processResponseStatus(status, onSuccess, onError, onMissing)
}

onMounted(async () => {
    projects.value = await fetchProjects()
    technologies.value = await fetchTechnologies()
    data.value = type == Type.task ? await fetchTask(id) : await fetchProject(id)
    formData.value = {
        name: data.value.name || "",
        description: data.value?.description || "",
        projectId: isTaskData(data.value) ? data.value.projectId : -1,
        technologyNames: new Array<string>
    }
    startDate.value = data.value.startDate == null ? "NEW" : new Date(data.value.startDate).toLocaleDateString()
    finishDate.value = data.value.finishDate == null ? "ONGOING" : new Date(data.value.finishDate).toLocaleDateString()
})
</script>
<template>
    <!-- Confirmation for deleting item -->
    <div v-if="viewConfirmation" class="cover bg-primary padding p-lg border border-thicker">
        <div class="close font-xxl" @click="viewConfirmation=false">X</div>
        <div class="cover-item absolute-center bg-primary border border-thicker padding p-lg flex justify-center direction-col text-center">
            <h3>CONFIRM DELETING ITEM</h3>
            <hr>
            <h3>Enter name and id of item to delete</h3>
            <p class="text-color-error font-xl" v-if="confirmationResult">{{ confirmationResult }}</p>
            <div>
                <input type="text" :placeholder="'NAME#ID'" class="font-xxl padding p-xs" v-model="confimation">
            </div>
            <button class="padding p-xs margin m-md" @click="handleDelete">CONFIRM</button>
        </div>
    </div>
    <!-- Main -->
    <div class="text-center">
        <!-- If editing: header -->
        <div v-if="isEditing">
            <h2>
                {{ type == Type.task ? "TASK" : "PROJECT" }} #{{ data.id }}
            </h2>
        </div>
        <form @submit.prevent="handleUpdate">
            <!-- If editing, input for name -->
            <div v-if="isEditing">
                <div>
                    <label for="name" class="font-xxl">NAME</label><br>
                    <input name="name" class="padding p-xs font-xl text-center" v-model="formData.name">
                </div>
            </div>
            <!-- If viewing details, show Name#Id -->
            <div v-else>
                <h2>{{ data.name }}<span class="absolute text-color-muted">#{{ data.id }}</span></h2>
                <p v-if="type == Type.task" class="font-xl">{{ (data as TaskDTO).projectName }}</p>
                <span class="margin-top m-xs block"></span>
                <hr class="width-50 block margin-left margin-right m-auto">
            </div>
            <!-- If editing, and viewed item is a task, select for project THE VALUE IS A STRING FFS WHY AM I RETARDED -->
            <div v-if="isEditing">
                <div v-if="type == Type.task">
                    <label for="projectName" class="font-xxl">PROJECT</label><br>
                    <select name="projectName" class="padding p-xs font-xl text-center" v-model="formData.projectId" required>
                        <option v-for="project in projects" :value="project.id">{{ project.name }}</option>
                    </select>
                </div>
                <div>
                    <label for="description" class="font-xxl">DESCRIPTION</label><br>
                    <textarea name="description" class="padding p-xs font-xl" v-model="formData.description"></textarea>
                </div>
            </div>
            <!-- If viewing details, show description -->
            <div v-else class="margin-top m-md">
                <p class="font-xxl text-bold">DESCRIPTION</p>
                <p class="text-justify padding p-lg width-40 margin-left margin-right m-auto">{{ data.description }}</p>
                <hr class="width-50 block margin-left margin-right m-auto">
            </div>
            <!-- If editing task, checkboxes for technologies -->
            <div v-if="isEditing">
                <div v-if="type == Type.task">
                    <p class="font-xxl">TECHNOLOGIES</p>
                    <div v-if="type == Type.task" class="flex justify-center margin-top m-xs select-none gap-sm wrap">
                        <div v-for="technology in technologies">
                            <label 
                                :for="technology.name"
                                class="border rounded padding p-xs font-xl checkbox"
                                :class="(data as TaskDTO).technologies.includes(technology.name) ? 'checked' : ''" 
                                @click="labelAsCheckbox($event.target as HTMLElement)"
                            >{{ technology.name }}</label>
                            <input type="checkbox" :value="technology.name" :name="technology.name" class="hidden" :checked="(data as TaskDTO).technologies.includes(technology.name)">
                        </div>
                    </div>
                </div>
            </div>
            <!-- If viewing details show technologies -->
            <div v-else-if="type == Type.task" class="flex justify-center margin-top m-md direction-col">
                <p class="font-xxl text-bold margin-bottom m-sm">TECHNOLOGIES</p>
                <div class="flex justify-center direction-row gap-md width-50 margin-left margin-right m-auto">
                    <div v-for="technology in (data as TaskDTO).technologies" class="border rounded padding p-xs font-xl">{{ technology }}</div>
                </div>
                <span class="margin-top m-md block"></span>
                <hr class="width-50 block margin-left margin-right m-auto">
            </div>
            <!-- Buttons if editing -->
            <div v-if="isEditing" class="margin-top m-xl flex justify-center gap-sm">
                <button class="padding p-xs font-xxl" type="submit">SAVE</button>
                <button class="padding p-xs font-xxl" @click="isEditing=false" type="button">CANCEL</button>
            </div>
            <!-- If not editing -->
            <div v-else >
                <p class="font-xxl text-bold margin-bottom margin-top m-sm">STATUS<span v-if="data.startDate" class="font-xxl text-bold"> AND DATE</span></p>
                <!-- Item status -->
                <p class="margin-top m-md font-xl">{{ statusToText(data.status) }}
                    <!-- Button to change status -->
                    <button type="button" @click="handleChangeStatus" class="absolute move margin-left m-xl padding p-xs" v-if="data.status < 2 && !blockStatusChange">
                        Move to {{ statusToText(data.status + 1) }}
                    </button>
                </p>
                <!-- Start - Finish date -->
                <p class="margin-top m-md font-xl" v-if="data.startDate">{{ startDate }} - {{ finishDate }}</p>
                <!-- Buttons -->
                <div class="margin-top m-xl flex justify-center gap-sm" v-if="data.status < 2">
                    <button class="padding p-xs font-xxl" type="button" @click="isEditing=true">EDIT</button>
                    <button class="padding p-xs font-xxl" type="button" @click="viewConfirmation=true">DELETE</button>
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