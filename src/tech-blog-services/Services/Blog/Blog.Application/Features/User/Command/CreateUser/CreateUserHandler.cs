using BuildingBlocks.Encryption;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Blog.Application.Features.User.Command.CreateUser
{
    public class CreateUserCommandHandler
        (IApplicationDbContext dbContext)
        : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            //get user from database using user query
            var matchingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == command.Email);

            //throw not found error if user not found
            if (matchingUser is not null)
            {
                throw new BadRequestException($"The User with email '{command.Email}' already exists");
            }

            //Create User Entity
            var user = Domain.Models.User.Create(id: UserId.Of(Guid.NewGuid()),
                                   firstName: command.FirstName,
                                   lastName: command.LastName,
                                   email: command.Email,
                                   password: BCryptEncryptor.HashPassword(command.Password),
                                   userRole: UserRole.Blogger);

            //Save to database
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateUserResult(true);
        }
    }
}
