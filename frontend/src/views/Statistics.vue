<script setup lang="ts">
import { onMounted, ref } from 'vue';
import {  } from '../functions/communication';
import type { TaskDTO, TechnologyDTO, ProjectDTO } from '../types/ItemData';

    const projects = ref<ProjectDTO[]>([])
    const tasks = ref<TaskDTO[]>([])
    const technologies = ref<TechnologyDTO[]>([])

    onMounted(async () => {
        projects.value = data.projects
        tasks.value = data.tasks
        technologies.value = data.technologies
    })
    
</script>
<template>
    {{ technologies }}
</template>
<style lang="css" scoped>
</style>