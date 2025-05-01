export type TaskDTO = {
    id: number
    projectId: number
    projectName: string 
    name: string 
    description: string
    status: number 
    startDate: string | null
    finishDate: string | null 
    technologies: string[]
}
export type ProjectDTO = {
    id: number 
    name: string 
    description: string 
    startDate: string | null
    finishDate: string | null 
    status: number
}
export type TechnologyDTO = {
    id: number
    name: string
}
export type CreateTaskDTO = {
    name: string
    description: string
    projectId: number
    technologyNames: string[]
}
export type CreateProjectDTO = {
    name: string
    description: string
}
export type CreateTechnologyDTO = {
    name: string
}