<script setup lang="ts">
    import type { CardData } from '../types/CardData'

    const props = defineProps<CardData>()
    
    const emit = defineEmits<{(event: 'send', payload: CardData): void}>()

    function sendProps() 
    {
        emit('send', 
        { 
            title: props.title, 
            project: props.project, 
            description: props.description, 
            start: props.start, 
            finish: props.finish, 
            technologies: props.technologies 
        })
    }
</script>

<template>
    <div @click="sendProps()" class="width-100 border round bg-surface padding p-sm card">
        <p class="font-lg text-bold"> {{ title }} </p>
        <p class="font-md text-color-muted"> {{ project }} </p>
    </div>
</template>

<style scoped>
    .card {
        width: 11rem;
        max-width: 100%;
        white-space: wrap;
        aspect-ratio: 2/1;
        cursor: pointer;
        transition: .5s;
        user-select: none;
    }
    .card:hover {
        opacity: .75;
    }
</style>

