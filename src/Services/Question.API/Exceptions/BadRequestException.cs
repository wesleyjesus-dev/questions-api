namespace Question.API.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message = "Payload with errors")
            : base(message)
        {
           
        }
    }
}
