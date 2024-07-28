namespace Blog.Application.Features.User.Queries.ValidateUser
{
    public record ValidateUserQuery(string Email, string Password)
        : IQuery<ValidateUserResult>;

    public record ValidateUserResult(UserDto User);
}
