<script setup lang="ts">
import { Bar } from 'vue-chartjs'
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale,
} from 'chart.js'

ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

const props = defineProps<{
  labels: string[]
  data: number[]
}>()

const total = props.data.reduce((acc, val) => acc + val, 0)
const percentData = props.data.map(v => (v / total) * 100)

const segmentColors = [
  '#f94144',
  '#43aa8b',
  '#577590',
  '#f8961e',
  '#90be6d',
  '#f9c74f'
]

const chartData = {
  labels: [''],
  datasets: props.labels.map((label, index) => ({
    label: label,
    data: [percentData[index]],
    backgroundColor: segmentColors[index],
    borderWidth: 0,
  }))
}

const chartOptions = {
  indexAxis: 'y' as const,
  responsive: true,
  maintainAspectRatio: false,
  scales: {
    x: {
      stacked: true,
      max: 100,
      display: false,
    },
    y: {
      stacked: true,
      display: true,
    }
  },
  plugins: {
    legend: {
      display: true
    },
  },
}
</script>

<template>
  <div class="chart-container">
    <Bar :data="chartData" :options="chartOptions" />
  </div>
</template>

<style scoped>
.chart-container {
  position: relative;
  width: 100%;
  max-height: 5rem;
}
</style>