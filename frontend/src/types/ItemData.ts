export type TaskData = {
    id: number
    projectName: string 
    name: string 
    description: string
    status: number 
    startDate: string 
    finishDate: string | null 
    technologies: string[]
}
export type ProjectData = {
    id: number 
    name: string 
    description: string 
    startDate: string
    finishDate: string | null 
    status: number
}
export type TechnologyData = {
    id: number
    name: string
}