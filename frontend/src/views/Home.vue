<script setup lang="ts">
    import Column from '../components/Column.vue' 
    import { ref, onMounted } from 'vue'
    import axios from 'axios'
    import type { CardData } from '../types/CardData'

    // Displaying the card on the middle of the screen with all the details
    // Got from Card.vue trough 
    const card = ref<CardData | null>(null)

    // All cards got from the server
    const cards = ref<any[]>([])

    const fetchTypes = ['tasks', 'projects']

    const fetchValue = ref<0 | 1>(0)

    const cardsByStatus = ref<Map<number, Array<CardData>>>(new Map());

    function closeCover() {
        card.value = null
    }

    function sortCardsByStatus()
    {
        for(let i = 0; i < cards.value.length; i++) 
        {
            const currentCard = cards.value[i]
            const status: number = currentCard.status
            const temp: CardData = 
            {
                description: currentCard.description,
                finish: currentCard.finishDate,
                project: currentCard.project || null,
                start: currentCard.startDate,
                technologies: currentCard.technologies,
                title: currentCard.name
            }
            
            addCard(status, temp)
        }
    }

    const addCard = (status: number, newCard: CardData) =>
    {
        if(!cardsByStatus.value.has(status))
        {
            cardsByStatus.value.set(status, [])
        }

        cardsByStatus.value.get(status)?.push(newCard)
    }

    const fetchItems = async (index: number) => 
    {
        try {
            const response = await axios.get('http://localhost:5000/api/todo/' + fetchTypes[index])
            cards.value = response.data
            sortCardsByStatus()
        } catch(error) {
            console.error(error)
        }
    }

    onMounted(async () => {
        fetchItems(fetchValue.value)
    })
</script>

<template>
    <div v-if="card" class="absolute cover" @click="closeCover">
        <div class="close">
            X
        </div>
        <div class="bg-surface absolute-center width-75 padding p-md round border border-thicker window">
            <div class="padding p-xl">
                <h1>{{ card.title }}</h1>
                <h1>{{ card.project }}</h1>
                <h1>{{ card.description }}</h1>
            </div>
        </div>
        
    </div>
    <div class="flex direction-col">
        <h1 class="text-center margin m-0">ToDo</h1>
        <h2 class="text-center margin m-xs" @click="() => {console.log(cardsByStatus.get(0))}">Tasks</h2>
        <div class="flex direction-col">
            <div class="width-90 margin-left margin-right m-auto flex gap-xxl columns">
                <Column 
                    title="NEW" 
                    :cards="cardsByStatus.get(0) || []"
                    @card-selected="card = $event"
                ></Column>
                <Column 
                    title="IN PROGRESS" 
                    :cards="cardsByStatus.get(1) || []"
                    @card-selected="card = $event"
                ></Column>
                <Column 
                    title="FINISHED"
                    :cards="cardsByStatus.get(2) || []"
                    @card-selected="card = $event"
                ></Column>
            </div>
        </div>
    </div>
</template>

<style scoped>
    .columns {
        max-height: 40rem;
        aspect-ratio: 3/1;
    }
    .cover {
        top: 0;
        left: 0;
        bottom:0;
        right:0;
        background-color: #000000af;
        z-index: 5;
    }
    .window {
        aspect-ratio: 2/1;
    }
    .close {
        position: absolute;
        top: 2%;
        right: 2%;
        font-size: 5rem;
        user-select: none;
        cursor: pointer;
        z-index: 6;
        transition: .5s;
    }
    .close:hover {
        color: var(--text-muted-color);
    }
</style>

