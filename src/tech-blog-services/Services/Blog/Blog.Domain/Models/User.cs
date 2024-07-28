namespace Blog.Domain.Models
{
    public class User : Entity<UserId>
    {
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string Password { get; private set; } = default!;
        public UserRole UserRole { get; private set; } = default!;

        public static User Create(UserId id,
                                  string firstName,
                                  string lastName,
                                  string email,
                                  string password,
                                  UserRole userRole)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(password);
            ArgumentNullException.ThrowIfNull(userRole);

            var user = new User
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                UserRole = userRole
            };

            return user;
        }
    }
}
