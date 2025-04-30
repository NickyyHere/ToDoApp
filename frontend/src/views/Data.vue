<script setup lang="ts">
import { onMounted, ref } from 'vue';
import type { ProjectDTO, TaskDTO, TechnologyDTO } from '../types/ItemData';
import { fetchProjects, fetchTasks, fetchTechnologies, importProject, importTask, importTechnology } from '../functions/communication';
import { generateExportJSON } from '../functions/utils';

const projects = ref<ProjectDTO[]>([])
const tasks = ref<TaskDTO[]>([])
const technologies = ref<TechnologyDTO[]>([])
const project_count = ref<number>(0)
const task_count = ref<number>(0)
const technology_count = ref<number>(0)
const JSONdata = ref<{
    projects: ProjectDTO[], 
    technologies: TechnologyDTO[], 
    tasks: TaskDTO[]
}>()
const isUpload = ref<boolean>(false)
const importStatus = ref<{success: number, fail: number}>({success: 0, fail: 0})

onMounted(async () => {
    projects.value = await fetchProjects()
    tasks.value = await fetchTasks()
    technologies.value = await fetchTechnologies()
    project_count.value = projects.value.length
    task_count.value = tasks.value.length
    technology_count.value = technologies.value.length
})
const generateJSON = async () => {
    JSONdata.value = await generateExportJSON(projects.value, technologies.value, tasks.value)
    isUpload.value = false
}
const downloadJSON = () => {
  const blob = new Blob([JSON.stringify(JSONdata.value, null, 2)], { type: 'application/json' });
  const url = URL.createObjectURL(blob);
  const a = document.createElement('a');
  a.href = url;
  a.download = 'export.json';
  document.body.appendChild(a);
  a.click();
  document.body.removeChild(a);
  URL.revokeObjectURL(url);
}
const handleFileUpload = async (event: Event) => {
    const target = event.target as HTMLInputElement
    const file = target.files?.[0]
    if (!file) return

    const reader = new FileReader()
    reader.onload = (e) => {
        try {
        JSONdata.value = JSON.parse(e.target?.result as string)
        } catch (err) {
        console.error('Invalid JSON', err)
        }
    }
    isUpload.value = true
    reader.readAsText(file)
}
const handleImport = async() => {
    JSONdata.value?.projects.forEach(async(project) => {
        if(await importProject(project) == 200) {
            importStatus.value.success++
        } else {
            importStatus.value.fail++
        }
    })
    JSONdata.value?.technologies.forEach(async(technology) => {
        if(await importTechnology(technology) == 200) {
            importStatus.value.success++
        } else {
            importStatus.value.fail++
        }
    })
    JSONdata.value?.tasks.forEach(async(task) => {
        if(await importTask(task) == 200) {
            importStatus.value.success++
        } else {
            importStatus.value.fail++
        }
    })
}
</script>
<template>
    <div>
        <p class="text-center font-xl">Project count: {{ project_count }}</p>
        <p class="text-center font-xl">Technology count: {{ technology_count }}</p>
        <p class="text-center font-xl">Task count: {{ task_count }}</p>
    </div>
    <div class="flex direction-col justify-center align-center">
        <button @click="generateJSON" class="padding p-xs text-center">Generate export data</button>
            <label for="import" class="font-xxl" accept="application/json">Import data</label>
            <input type="file" name="import" @change="handleFileUpload" class="text-center">
    </div>
    <div v-if="JSONdata" class="margin-top m-xl">
        <button @click="downloadJSON" class="padding p-xs text-center block margin-left margin-right m-auto" v-if="!isUpload">Export</button>
        <button @click="handleImport" class="padding p-xs text-center block margin-left margin-right m-auto" v-if="isUpload">Import</button>
        <p class="text-center" v-if="importStatus.fail != 0 && importStatus.success != 0">Successful imports: {{ importStatus.success }}, Failed imports: {{ importStatus.fail }}</p>
        <span class="margin m-xs"></span>
        <textarea class="width-75 block margin-left margin-right m-auto json" readonly>
            {{ JSON.stringify(JSONdata, null, 2) }}
        </textarea>
    </div>
</template>
<style lang="css" scoped>
.json {
    min-width: 75%;
    max-width: 75%;
    min-height: 25rem;
    resize: none;
}
.json::-webkit-scrollbar {
    width:5px;
}
.json::-webkit-scrollbar-track {
    width: 1rem;
    background-color: var(--surface-color);
}
.json::-webkit-scrollbar-thumb {
    background-color: var(--accent-color);
}
</style>