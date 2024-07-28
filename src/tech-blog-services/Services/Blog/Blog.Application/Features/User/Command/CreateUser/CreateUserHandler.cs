using BuildingBlocks.Encryption;

namespace Blog.Application.Features.User.Command.CreateUser
{
    public class CreateUserCommandHandler
        (IApplicationDbContext dbContext)
        : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
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
            catch (Exception ex) {

                throw new InternalServerException("There was something wrong with the server: " + ex.InnerException.Message);
            }
        }
    }
}
