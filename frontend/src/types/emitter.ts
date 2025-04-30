import mitt from 'mitt'
import type { MessageType } from './types'

type Events = {
    showNotification: {messageType: MessageType, message: string}
}
const emitter = mitt<Events>()
export default emitter