import router from "../router";
import type { ProjectDTO, TaskDTO } from "../types/ItemData";

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

export function processResponseStatus(status: number, onSuccess?: () => void, onError?: () => void, onMissing?: () => void) {
    switch(status) {
        case 200:
            onSuccess?.()
            break
        case 404:
            onMissing?.()
            break
        case 400:
            onError?.()
            break
    }
}