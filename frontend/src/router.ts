import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import Todo from './views/Todo.vue';
import Details from './views/Details.vue';
import AddNew from './views/Add-new.vue';
import Statistics from './views/Statistics.vue';


const routes: Array<RouteRecordRaw> = 
[
  {path: "/", component: Todo },
  {path: "/details", component: Details},
  {path: "/add-new", component: AddNew},
  {path: "/statistics", component: Statistics} 
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router;