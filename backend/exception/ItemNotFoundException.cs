namespace ToDoApp.exception
{
    [Serializable]
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() { }
        public ItemNotFoundException(string message) : base(message) { }
        public ItemNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected ItemNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            #pragma warning disable SYSLIB0051 // Type or member is obsolete
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
