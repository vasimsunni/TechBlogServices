namespace Blog.Application.Helpers
{
    public class UserContext : IUserContext
    {
        static (string UserId, string UserRole) User;
        public void SetUser(string userId, string userRole)
        {
            User.UserId = userId;
            User.UserRole = userRole;
        }

        public (string UserId, string UserRole) GetUser() => User;
    }
}
