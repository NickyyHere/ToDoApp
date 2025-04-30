<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { fetchProjects, fetchTasks, fetchTechnologies } from '../functions/communication';
import type { TaskDTO, TechnologyDTO, ProjectDTO } from '../types/ItemData';
import { countCompletion, countProjectStatistics, countTechnologyUsage } from '../functions/statistics';
import DoughnutChart from '../components/DoughnutChart.vue';
import BarChart from '../components/BarChart.vue';
import { statusToText } from '../functions/utils';

    const projects = ref<ProjectDTO[]>([])
    const tasks = ref<TaskDTO[]>([])
    const technologies = ref<TechnologyDTO[]>([])

    const technologyUsage = ref<Record<string, number>>({})
    const projectStatistics = ref<Record<string, { newTasks: number; inProgressTasks: number; finishedTasks: number, technologies: Record<string, number>}>>({})
    const projectCompletion = ref<Record<number,number>>({})
    const taskCompletion = ref<Record<number,number>>({})

    onMounted(async () => {
        projects.value = await fetchProjects()
        tasks.value = await fetchTasks()
        technologies.value = await fetchTechnologies()
        countTechnologyUsage(tasks.value).then(result => {technologyUsage.value = result})
        countProjectStatistics(tasks.value).then(result => {projectStatistics.value = result})
        countCompletion(projects.value).then(result => {projectCompletion.value = result})
        countCompletion(tasks.value).then(result => {taskCompletion.value = result})
    })
    
</script>
<template>
    <div>
        <div>
            <h2 class="text-center">Global project status</h2>
        </div>
        <div v-if="Object.keys(projectCompletion).length > 0">
            <BarChart
                :labels="Object.keys(projectCompletion).map(t => statusToText(parseInt(t)))"
                :data="Object.values(projectCompletion)"
            />
        </div>
        <div>
            <hr>
            <h2 class="text-center">Global task status</h2>
        </div>
        <div v-if="Object.keys(taskCompletion).length > 0">
            <BarChart
                :labels="Object.keys(taskCompletion).map(t => statusToText(parseInt(t)))"
                :data="Object.values(taskCompletion)"
            />
        </div>
        <div>
            <hr>
            <h2 class="text-center">Technology usage across projects</h2>
        </div>
        <div class="width-40 margin-left margin-right m-auto min-chart-size">
            <DoughnutChart
            v-if="Object.keys(technologyUsage).length > 0"
            :labels="Object.keys(technologyUsage)"
            :data="Object.values(technologyUsage)"
            :show-labels="true"
            />
        </div>
        <div>
            <hr>
            <h2 class="text-center">Task status per project</h2>
        </div>
        <div class="flex width-75 margin-left margin-right m-auto wrap gap-xxxl align-start justify-center">
            <div v-for="(project, name) in projectStatistics" class="width-100 chart min-chart-size">
                <p class="text-center font-xl">{{ name }}</p>
                <DoughnutChart
                    :labels="['New', 'In Progress', 'Finished']"
                    :data="[project.newTasks, project.inProgressTasks, project.finishedTasks]"
                    :show-labels="true"
                />
            </div>
        </div>
    </div>
    
    
    
</template>
<style lang="css" scoped>
.chart {
    flex: 1 1 30%;
    max-width: 33%;
    box-sizing: border-box;
}
.min-chart-size {
    min-width: 15rem;
}
</style>