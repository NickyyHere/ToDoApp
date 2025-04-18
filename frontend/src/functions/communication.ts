import type { ProjectData, TaskData, TechnologyData } from "../types/ItemData";
import axios from 'axios'

export async function fetchItems(type: string) : Promise<TaskData[] | ProjectData[] | TechnologyData[]> {
    try {
        const response = await axios.get(`http://localhost:5000/api/todo/${type}`)
        return response.data
    } catch(error) {
        console.error(`Error fetching data: ${error}`)
        return []
    }
}

export async function addNewItem(type: string, data: TaskData | ProjectData | string, projectId: number | null = null) {
    try {
        let url = `http://localhost:5000/api/todo/${type}`
        if(projectId != null) {
            url += `/${projectId}`
        }
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

export async function updateItem(type: string, data: TaskData | ProjectData, id: number) {
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

export async function deleteItem(type: string, id: number) {
    try {
        let url = `http://localhost:5000/api/todo/${type}/${id}`
        const response = await axios.delete(url)
        return response.status
    } catch(error) {
        console.error(`Error updating ${type}: ${error}`)
    }
}

export async function changeItemStatus(type: string, id: number) {
    try {
        let url = `http://localhost:5000/api/todo/${type}/status/${id}`
        const response = await axios.put(url)
        return response.status
    } catch(error) {
        console.error(`Error updating ${type}: ${error}`)
    }
}