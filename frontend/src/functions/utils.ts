import router from "../router";
import emitter from "../types/emitter";
import type { ProjectDTO, TaskDTO, TechnologyDTO } from "../types/ItemData";
import { MessageType } from "../types/types";
export async function filterDataByStatus(data: TaskDTO[] | ProjectDTO[], status: number) : Promise<TaskDTO[] | ProjectDTO[]>{
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

export function isTaskData(obj: any) : obj is TaskDTO {
    return (
        obj &&
        typeof obj.projectName === 'string'
    )
}

export function labelAsCheckbox(target: HTMLElement) {
    const checkbox = target.closest('div')?.querySelector('input[type="checkbox"]') as HTMLInputElement
    checkbox.checked = !checkbox.checked
    target.classList.toggle('checked')
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

export function truncateText(text: string, maxLength: number): string {
    return text.length > maxLength ? text.slice(0, maxLength) + '...' : text;
}

export function processResponseStatus(status: number, onSuccess?: () => void) {
    switch(status) {
        case 200:
            onSuccess?.()
            break            
    }
}
export function processResponseError(status: number, message: string)
{
    console.error(`${status}: ${message}`)
    emitter.emit("showNotification", {messageType: MessageType.error, message: `${status}: ` + message || "Something unexpected happened"})
}

export async function generateExportJSON(projects?: ProjectDTO[], technologies?: TechnologyDTO[], tasks?: TaskDTO[]) : Promise<{
    projects: ProjectDTO[], 
    technologies: TechnologyDTO[], 
    tasks: TaskDTO[]
}> {
    const exportJSON: {
        projects: ProjectDTO[]
        technologies: TechnologyDTO[]
        tasks: TaskDTO[]
    } = {
        "projects": [],
        "technologies": [],
        "tasks": []
    }
    if(projects != null) {
        projects.forEach(project => {
            exportJSON.projects.push({
                id: project.id,
                name: project.name,
                description: project.description,
                startDate: project.startDate,
                finishDate: project.finishDate,
                status: project.status
            })
        })
    }
    if(technologies != null) {
        technologies.forEach(technology => {
            exportJSON.technologies.push({
                id: technology.id,
                name: technology.name
            })
        })
    }
    if(tasks != null)
        
    tasks.forEach(task => {
        exportJSON.tasks.push({
            id: task.id,
            projectId: task.projectId,
            name: task.name,
            description: task.description,
            projectName: task.projectName,
            technologies: task.technologies,
            startDate: task.startDate,
            finishDate: task.finishDate,
            status: task.status
        })
    })
    return exportJSON
}
