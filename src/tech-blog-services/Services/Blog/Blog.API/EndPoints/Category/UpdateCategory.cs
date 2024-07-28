using Blog.Application.Features.Category.Command.UpdateCategory;

namespace Blog.API.EndPoints.Category
{
    //Accepts a UpdateCategoryRequest object
    //Maps the request to a UpdateCategoryCommand handler
    //Uses MediatR to send the command to the corresponding handler
    //Returns a success or error response based on the outcome

    public record UpdateCategoryRequest(Guid Id, string CategoryName);
    public record UpdateCategoryResponse(bool IsSuccess);
    public class UpdateCategory : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/categories", async (UpdateCategoryRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateCategoryCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateCategoryResponse>();

                return Results.Ok(response);
            })
            .RequireAuthorization(nameof(UserRole.Admin))
            .AddEndpointFilter<UserSetterFilter>()
            .WithName("UpdateCategory")
            .Produces<UpdateCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Category")
            .WithDescription("Update Category");
        }
    }
}
