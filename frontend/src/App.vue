<script setup lang="ts">
  import Header from './components/Header.vue'
  import emitter from './types/emitter';
  import Notification from './components/Notification.vue';
import { ref } from 'vue';
import { MessageType } from './types/types';

  const notificationMessage = ref<string>("")
  const notificationType = ref<MessageType | null>(null)

  emitter.on('showNotification', ({messageType, message}) => {
    notificationMessage.value = message
    notificationType.value = messageType
    setTimeout(() => {notificationType.value = null}, 3000)
  })
</script>

<template>
  <Notification v-if="notificationType" :message="notificationMessage" :type="notificationType"/>
  <div>
    <Header class="margin-bottom m-lg"></Header>
    <div class="margin-left margin-right m-lg">
        <RouterView />
    </div>
  </div>
</template>

<style lang="css" scoped>
</style>