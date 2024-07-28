
using Blog.Application.Features.User.Command.CreateUser;

namespace Blog.API.EndPoints.User
{
    //Accepts an FirstName, LastName, Email and Password as request
    //Maps the request to a CreateUserCommand handler
    //Uses MediatR to send the command to the corresponding handler
    //Returns a response with success

    public record RegisterUserRequest(string FirstName, string LastName, string Email, string Password);
    public record RegisterUserResponse(bool IsSuccess);

    public class RegisterUser : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/users/register/blogger", async (RegisterUserRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateUserCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<RegisterUserResponse>();

                return Results.Created("", response);
            })
            .WithName("RegisterBlogger")
            .Produces<LoginUserResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Register Blogger")
            .WithDescription("Register Blogger");
        }
    }
}
