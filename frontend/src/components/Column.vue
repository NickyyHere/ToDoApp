<script setup lang="ts">
    import Card from "./Card.vue"
    import type { CardData } from "../types/CardData"

    const props = defineProps<{title: string, cards: CardData[]}>()

    const emit = defineEmits<{(event: 'card-selected', card: CardData): void}>()

    function sendCard(card: any) 
    {
        emit('card-selected', card)
    }
</script>
<template>
    <div class="flex direction-col text-center width-100 border border-thicker rounded">
        <h3 class="margin m-0 padding p-sm" @click="() => {console.log(props)}">
            {{ title }}
           <hr> 
        </h3>
        <div class="flex justify-center padding p-md wrap gap-sm cards">
            <Card v-for="card in props.cards"
                :title = "card.title"
                :description="card.description"
                :project="card.project"
                :start="card.start"
                :finish="card.finish"
                :technologies="card.technologies"
                @send="sendCard"
            ></Card>
        </div>
    </div>
</template>
<style scoped>
    .cards {
        overflow-y: scroll;
    }
    ::-webkit-scrollbar {
        width: 8px;
    }
    ::-webkit-scrollbar-track {
        background-color: var(--surface-color);
        border-radius: 20px;
    }
    ::-webkit-scrollbar-thumb {
        background-color: var(--accent-color);
        border-radius: 20px;
    }

</style>