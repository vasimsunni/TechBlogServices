namespace Blog.Application.Extensions
{
    public static class UserExtentions
    {
        public static UserDto ToUserDto(this Domain.Models.User user)
        {
            return new UserDto(
                Id: user.Id.Value,
                FirstName: user.FirstName,
                LastName: user.LastName,
                Email: user.Email,
                UserRole: user.UserRole.ToString()
                );
        }
    }
}
