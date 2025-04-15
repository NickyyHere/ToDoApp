import router from "../router";
import type { ProjectData, TaskData, TechnologyData } from "../types/ItemData";

export async function filterDataByStatus(data: TaskData[] | ProjectData[], status: number) : Promise<TaskData[] | ProjectData[]>{
    return data.filter(item => item.status == status)
}

export function redirect(url: string) {
    router.push(url)
}

export function formToTypeData(type: string, formData: {
    name: string
    description: string
    project: number
    technologies: TechnologyData[]
}) : TaskData | ProjectData | string {
    switch(type) {
        case "tasks":
            return {
                name: formData.name,
                description: formData.description,
                technologies: formData.technologies,
                startDate: new Date().toISOString(),
                finishDate: null,
                projectName: '',
                id: -1,
                status: 0
            } as TaskData
        case "projects":
            return {
                name: formData.name,
                description: formData.description,
                id: -1,
                status: 0,
                startDate: new Date().toISOString(),
                finishDate: null
            } as ProjectData
        case "technologies":
            return formData.name
            
    }
    return ""
}