namespace Blog.Application.Helpers
{
    public interface IUserContext
    {
        void SetUser(string userId, string userRole);
        (string UserId, string UserRole) GetUser();
    }
}
