import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'
import Todo from './views/Todo.vue';
import Details from './views/Details.vue';
import AddNew from './views/Add-new.vue';


const routes: Array<RouteRecordRaw> = 
[
  {path: "/", component: Todo },
  {path: "/details", component: Details, props: true },
  {path: "/add-new", component: AddNew}  
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router;