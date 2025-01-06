using Blog.API.Helpers;
using Blog.Application.Features.User.Queries.ValidateUser;


namespace Blog.API.EndPoints.User
{
    //Accepts an Email and  a Password
    //Maps the request to a ValidateUserQuery handler
    //Uses MediatR to send the command to the corresponding handler
    //Returns a response with the created token

    public record LoginUserRequest(string Email, string Password);
    public record LoginUserResponse(bool IsSuccess, string token);
    public class LoginUser : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/users/login", async (LoginUserRequest request, ISender sender, ILogger<LoginUser> logger) =>
            {
                logger.LogInformation("User loggedin with email:"+request.Email);

                var command = request.Adapt<ValidateUserQuery>();

                var result = await sender.Send(command);

                string token = JwtTokenHelper.GenerateToken(result.User.Id.ToString(), result.User.UserRole);

                var response = new LoginUserResponse(true, token);

                return Results.Ok(response);
            })
            .WithName("LoginUser")
            .Produces<LoginUserResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Login User")
            .WithDescription("Login User");
        }
    }
}
