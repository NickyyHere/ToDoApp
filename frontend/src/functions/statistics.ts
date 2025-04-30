import type { ProjectDTO, TaskDTO } from "../types/ItemData";

export async function countTechnologyUsage(tasks: TaskDTO[]) : Promise<Record<string, number>>{
    const counts: Record<string, number> = {}
    tasks.forEach(task => {
        task.technologies.forEach(technology => {
            if (technology in counts) {
                counts[technology]++
            } else {
                counts[technology] = 1
            }
        })
    })
    return counts
}

export async function countProjectStatistics(tasks: TaskDTO[]) {
    type ProjectStats = {
        newTasks: number
        inProgressTasks: number
        finishedTasks: number
        technologies: Record<string, number>
    }
    const summary: Record<string, ProjectStats> = {}
    tasks.forEach(task => {
        const projectName = task.projectName
        if(!(projectName in summary)) {
            summary[projectName] = {
                newTasks: 0,
                inProgressTasks: 0,
                finishedTasks: 0,
                technologies: {}
            }
        }
        switch(task.status) {
            case 0:
                summary[projectName].newTasks++
                break
            case 1:
                summary[projectName].inProgressTasks++
                break
            case 2:
                summary[projectName].finishedTasks++
                break
        }
        task.technologies.forEach(technology => {
            if(technology in summary[projectName].technologies) {
                summary[projectName].technologies[technology]++
            } else {
                summary[projectName].technologies[technology] = 1
            }
        })
    })
    return summary
}

export async function countCompletion(toCount: TaskDTO[] | ProjectDTO[]) : Promise<Record<number, number>> {
    const stats: Record<number, number> = {}
    toCount.forEach(element => {
        if(element.status in stats) {
            stats[element.status]++
        } else {
            stats[element.status] = 1
        }
    })
    return stats
}