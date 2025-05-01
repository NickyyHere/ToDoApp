 namespace ToDoApp.exception
 {
    [Serializable]
    public class ItemAlreadyFinishedException : Exception
    {
        public ItemAlreadyFinishedException() { }
        public ItemAlreadyFinishedException(string message) : base(message) { }
        public ItemAlreadyFinishedException(string message, Exception inner) : base(message, inner) { }
        protected ItemAlreadyFinishedException(
            System.Runtime.Serialization.SerializationInfo info,
            #pragma warning disable SYSLIB0051
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
 }