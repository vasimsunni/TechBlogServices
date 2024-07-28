using BuildingBlocks.Encryption;

namespace Blog.Application.Features.User.Queries.ValidateUser
{
    public class ValidateUserQueryHandler
        (IApplicationDbContext dbContext)
        : IQueryHandler<ValidateUserQuery, ValidateUserResult>
    {
        public async Task<ValidateUserResult> Handle(ValidateUserQuery query, CancellationToken cancellationToken)
        {
            //get user from database using user query
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == query.Email);

            //throw not found error if user not found
            if (user == null)
            {
                throw new UserNotFoundException(query.Email);
            }

            //verify user password
            bool isValid = BCryptEncryptor.VerifyPassword(query.Password, user.Password);

            //throw error if invalid user
            if (!isValid)
            {
                throw new InvalidUserException(query.Email);
            }

            //return user if verified
            return new ValidateUserResult(user.ToUserDto());
        }
    }
}
