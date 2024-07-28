
namespace Blog.Application.Exceptions
{
    public class InvalidUserException : BadRequestException
    {
        public InvalidUserException(string email) : base($"User credentials does not match with Email:{email}")
        {
        }
    }
}
