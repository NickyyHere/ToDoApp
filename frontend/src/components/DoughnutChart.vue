<script setup lang="ts">
import { Doughnut } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  ArcElement,
} from 'chart.js'
import { ref, onMounted, onBeforeUnmount } from 'vue'
import type { Chart } from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, ArcElement)

const props = defineProps<{
  labels: string[],
  data: number[],
  showLabels?: boolean
}>()

const chartRef = ref<InstanceType<typeof Doughnut> & { chart?: Chart }>()

const baseColors = ['#f87979', '#7cc6fe', '#b8e986', '#ffd166', '#06d6a0', '#118ab2', '#ef476f', '#ef1515', '#110ff0']

const backgroundColors = Array.from({ length: props.data.length }, (_, i) => baseColors[i % baseColors.length])

const chartData = {
  labels: props.labels,
  datasets: [
    {
      backgroundColor: backgroundColors,
      data: props.data,
      borderWidth: 0
    }
  ]
}

const chartOptions = {
  responsive: true,
  maintainAspectRatio: true,
  plugins: {
    legend: {
      display: props.showLabels ?? false,
    },
  },
}

const handleResize = () => {
  if (chartRef.value?.chart) {
    chartRef.value.chart.resize()
  }
}

onMounted(() => {
  window.addEventListener('resize', handleResize)
})

onBeforeUnmount(() => {
  window.removeEventListener('resize', handleResize)
})
</script>

<template>
  <div class="chart-container">
    <Doughnut 
      ref="chartRef"
      :data="chartData" 
      :options="chartOptions" 
    />
  </div>
</template>

<style scoped>
.chart-container {
  position: relative;
  width: 100%;
  height: 100%;
}
</style>