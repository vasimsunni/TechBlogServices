using Blog.Application.Features.Category.Command.DeleteCategory;

namespace Blog.API.EndPoints.Category
{
    //Accepts the CategoryId from parameter
    //Constructs a DeleteCategoryCommand
    //Sends the command using MediatR
    //Returns a success or not found response

    //public record DeleteCategoryRequest(Guid CategoryId);
    public record DeleteCategoryResponse(bool IsSuccess);
    public class DeleteCategory : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/categories/{CategoryId}", async (Guid CategoryId, ISender sender) =>
            {
                var result = await sender.Send(new DeleteCategoryCommand(CategoryId));

                var response = result.Adapt<DeleteCategoryResponse>();

                return Results.Ok(response);
            })
            .RequireAuthorization(nameof(UserRole.Admin))
            .AddEndpointFilter<UserSetterFilter>()
            .WithName("DeleteCategory")
            .Produces<DeleteCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Category")
            .WithDescription("Delete Category");
        }
    }
}
