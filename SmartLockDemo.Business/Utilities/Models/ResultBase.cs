namespace SmartLockDemo.Business.Utilities
{
    /// <summary>
    /// Base model for operation result models
    /// </summary>
    public class ResultBase
    {
        /// <summary>
        /// Specifies operation is successful or not
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Any kind of message that is wanted to deliver
        /// </summary>
        public object Message { get; set; }

        public ResultBase(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public ResultBase(bool isSuccessful, object message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
