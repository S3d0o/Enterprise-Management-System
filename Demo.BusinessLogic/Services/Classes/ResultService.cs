namespace Demo.BusinessLogic.Services.Classes
{
    public class ResultService
    {
        public bool Success { get; private set; }
        public string Message { get; private set; } = string.Empty;

        public ResultService(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static ResultService Ok(string Message = "") => new (true, Message); // Success case
        public static ResultService Fail(string Message = "") => new (false, Message); // Failure case 
    }
}
