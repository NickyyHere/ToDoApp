import { Type, Action } from "../types/types";
import { addNewProject, addNewTask, addNewTechnology, updateProject, updateTask } from "./communication";

export async function sendForm(action: Action, type: Type, formData: any, id: number | null = null) : Promise<number> {
    switch(type) {
        case Type.project:
            return action === Action.add ? await addNewProject(formData) : await updateProject(formData, id as number)
        case Type.task:
            return action === Action.add ? await addNewTask(formData) : await updateTask(formData, id as number)
        case Type.technology:
            return action === Action.add ? await addNewTechnology(formData) : 400
        default:
            return 400
    }
}