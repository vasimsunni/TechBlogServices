
using Blog.Application.Features.Category.Command.CreateCategory;
using Blog.Domain.Enums;

namespace Blog.API.EndPoints.Category
{
    //Accepts a CreateCategoryRequest object
    //Maps the request to a CreateCategoryCommand handler
    //Uses MediatR to send the command to the corresponding handler
    //Returns a response with the created Category's Id

    public record CreateCategoryRequest(string CategoryName);
    public record CreateCategoryResponse(Guid Id);

    public class CreateCategory : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/categories", async (CreateCategoryRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateCategoryCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateCategoryResponse>();

                return Results.Created($"/categories/{response.Id}", response);
            })
            .RequireAuthorization(nameof(UserRole.Admin))
            .AddEndpointFilter<UserSetterFilter>()
            .WithName("CreateCategory")
            .Produces<CreateCategoryResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Category")
            .WithDescription("Create Category");
        }
    }
}
