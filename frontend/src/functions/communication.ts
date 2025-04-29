import type { CreateProjectDTO, CreateTaskDTO, CreateTechnologyDTO, ProjectDTO, TaskDTO, TechnologyDTO } from "../types/ItemData";
import axios from 'axios'
import { Type } from "../types/types";

async function fetchItems(type: Type) {
    try {
        const response = await axios.get(`http://localhost:5000/api/todo/${type}`)
        return response.data
    } catch(error) {
        console.error(`Error fetching data: ${error}`)
        return []
    }
}

async function addNewItem(type: Type, data: CreateTaskDTO | CreateProjectDTO | CreateTechnologyDTO) {
    try {
        let url = `http://localhost:5000/api/todo/${type}`
        const response = await axios.post(url, JSON.stringify(data),
    {
        headers: {
            "Content-Type": "application/json"
        }
    })
    return response.status as number
    } catch(error) {
        console.error(`Error adding new ${type}: ${error}`)
    }
}

async function updateItem(type: Type, data: CreateTaskDTO | CreateProjectDTO, id: number) {
    try {
        let url = `http://localhost:5000/api/todo/${type}/${id}`
        const response = await axios.put(url, JSON.stringify(data),
    {
        headers: {
            "Content-Type": "application/json"
        }
    })
    return response.status
    } catch(error) {
        console.error(`Error updating ${type}: ${error}`)
    }
}

async function deleteItem(type: Type, id: number) {
    try {
        let url = `http://localhost:5000/api/todo/${type}/${id}`
        const response = await axios.delete(url)
        return response.status
    } catch(error) {
        console.error(`Error updating ${type}: ${error}`)
    }
}

async function changeItemStatus(type: Type, id: number) {
    try {
        let url = `http://localhost:5000/api/todo/${type}/status/${id}`
        const response = await axios.put(url)
        return response.status
    } catch(error) {
        console.error(`Error updating ${type}: ${error}`)
    }
}

export async function fetchTasks() : Promise<TaskDTO[]> {
    return fetchItems(Type.task)
}
export async function fetchProjects() : Promise<ProjectDTO[]> {
    return fetchItems(Type.project)
}
export async function fetchTechnologies() : Promise<TechnologyDTO[]> {
    return fetchItems(Type.technology)
}
export async function addNewTask(data: CreateTaskDTO) : Promise<number> {
    return await addNewItem(Type.task, data) as number
}
export async function addNewProject(data: CreateProjectDTO) : Promise<number> {
    return await addNewItem(Type.project, data) as number
}
export async function addNewTechnology(data: CreateTechnologyDTO) : Promise<number> {
    return await addNewItem(Type.technology, data) as number
}
export async function updateTask(data: CreateTaskDTO, id: number) : Promise<number> {
    return await updateItem(Type.task, data, id) as number
}
export async function updateProject(data: CreateProjectDTO, id: number) : Promise<number> {
    return await updateItem(Type.project, data, id) as number
}
export async function changeStatus(type: Type, id: number) : Promise<number> {
    return await changeItemStatus(type, id) as number
}
export async function deleteTask(id: number) : Promise<number> {
    return await deleteItem(Type.task, id) as number 
}
export async function deleteProject(id: number) : Promise<number> {
    return await deleteItem(Type.project, id) as number
}
export async function changeTaskStatus(id: number) : Promise<number> {
    return await changeItemStatus(Type.task, id) as number
}
export async function changeProjectStatus(id: number) : Promise<number> {
    return await changeItemStatus(Type.project, id) as number
}

