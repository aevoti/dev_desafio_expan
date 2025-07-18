namespace DesafioAEVO.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : ExceptionBase
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
