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
        await axios.post(url, JSON.stringify(data),
    {
        headers: {
            "Content-Type": "application/json"
        }
    })
    } catch(error) {
        console.error(`Error adding new ${type}: ${error}`)
    }
}

export async function updateItem(type: string, data: TaskData | ProjectData, id: number) {
    try {
        let url = `http://localhost:5000/api/todo/${type}/${id}`
        axios.put(url, JSON.stringify(data),
    {
        headers: {
            "Content-Type": "application/json"
        }
    })
    } catch(error) {
        console.error(`Error updating ${type}: ${error}`)
    }
}

export async function deleteItem(type: string, id: number) {
    try {
        let url = `http://localhost:5000/api/todo/${type}/${id}`
        await axios.delete(url)
    } catch(error) {
        console.error(`Error updating ${type}: ${error}`)
    }
}