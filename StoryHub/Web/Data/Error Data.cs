namespace Web.Data
{
    public class ErrorData
    {
        /// <summary>
        /// Create an object to store the error message
        /// </summary>
        public ErrorData(string error_message)
        {
            this.ErrorMessage = error_message;
        }

        /// <summary>
        /// Return the error message
        /// </summary>
        public string ErrorMessage { get; }
    }
}
