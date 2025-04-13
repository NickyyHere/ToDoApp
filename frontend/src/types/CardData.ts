export type CardData = {
    title: string
    project: string | null
    description: string
    start: string
    finish: string | null
    technologies: string[]
}
export const cardTemplate: CardData = {
    title: '',
    project: null,
    description: '',
    start: '',
    finish: null,
    technologies: []
}