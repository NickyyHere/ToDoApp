import router from "../router";
import type { ProjectData, TaskData } from "../types/ItemData";

export async function filterDataByStatus(data: TaskData[] | ProjectData[], status: number) : Promise<TaskData[] | ProjectData[]>{
    return data.filter(item => item.status == status)
}

export function redirect(options: string | { url: string, props?: any }) {
    if (typeof options === 'string') {
      router.push(options)
    } else {
      const query = options.props ? { data: JSON.stringify(options.props) } : undefined
      router.push({ path: options.url, query })
    }
  }

export function formToTypeData(type: string, formData: {
    name: string
    description: string
    project: number
    technologies: string[]
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

export function formToTypeDataUpdate(type: string, formData: {
    name: string
    description: string
    projectName: string
    technologies: string[]
}) : TaskData | ProjectData {
    switch(type) {
        case "tasks":
            return {
                name: formData.name,
                description: formData.description,
                technologies: formData.technologies,
                startDate: new Date().toISOString(),
                finishDate: null,
                projectName: formData.projectName,
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
    }
    return {} as TaskData
}

export function isTaskData(obj: any) : obj is TaskData {
    return (
        obj &&
        typeof obj.projectName === 'string'
    )
}

export function labelAsCheckbox(target: HTMLElement, checkedTechnologies: string[]) {
    const checkbox = target.closest('div')?.querySelector('input[type="checkbox"]') as HTMLInputElement
    checkbox.checked = !checkbox.checked
    target.classList.toggle('checked')
    const technology: string = checkbox.name
    let index: number | null = null
    if(checkedTechnologies.length == 0) {
        checkedTechnologies.push(technology)
    } else {
        for(let i = 0; i < checkedTechnologies.length; i++) {
            if(checkedTechnologies[i] == technology) {
                index = i
                break
            }
        }

        if(index == null) {
            checkedTechnologies.push(technology)
        } else {
            checkedTechnologies.splice(index, 1)
        }
    }
}

export function statusToText(status: number) : string {
    switch(status) {
        case 0:
            return "NEW"
        case 1:
            return "IN PROGRESS"
        case 2:
            return "FINISHED"
        default:
            return "UNKNOWN"
    }
}