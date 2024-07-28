
namespace Blog.Application.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string email) : base("User", email)
        {
        }
    }
}
