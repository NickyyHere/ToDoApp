export enum Type {
    project = "projects",
    task = "tasks",
    technology = "technologies"
}
export enum Action {
    add,
    update,
    statusUpdate,
    fetch,
    delete
}

export enum MessageType {
    notification = "text-color-primary",
    success = "text-color-success",
    error = "text-color-error"
}