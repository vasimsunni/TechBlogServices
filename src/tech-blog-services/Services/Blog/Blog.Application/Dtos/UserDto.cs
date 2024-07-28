namespace Blog.Application.Dtos
{
    public record UserDto(Guid Id,
                         string FirstName,
                         string LastName,
                         string Email,
                         string UserRole);
}
