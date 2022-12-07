namespace PgAPI.Domain
{
    public class ValidationMessage
    {
        public ValidationMessage(string code, string message)
        {
            Code = code;
            Message = message;
        }
        public string Code { get; }
        public string Message { get; set; }
    }
}