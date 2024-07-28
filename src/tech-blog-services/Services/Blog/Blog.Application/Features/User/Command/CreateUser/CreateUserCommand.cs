
namespace Blog.Application.Features.User.Command.CreateUser
{
    public record CreateUserCommand(string FirstName, string LastName, string Email, string Password)
       : ICommand<CreateUserResult>;

    public record CreateUserResult(bool IsSuccess);

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
