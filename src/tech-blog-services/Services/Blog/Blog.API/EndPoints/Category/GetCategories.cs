using Blog.Application.Features.Category.Query.GetCategories;
using BuildingBlocks.Pagination;

namespace Blog.API.EndPoints.Category
{
    //public record GetCategoriesRequest(PaginationRequest PaginationRequest);
    public record GetCategoriesResponse(PaginatedResult<CategoryDto> Categories);

    public class GetCategories : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/categories", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetCategoriesQuery(request));

                var response = result.Adapt<GetCategoriesResponse>();

                return Results.Ok(response);
            })
             .RequireAuthorization("AdminAndBlogger")
             .AddEndpointFilter<UserSetterFilter>()
             .WithName("GetCategories")
             .Produces<GetCategoriesResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Categories with Pagination")
             .WithDescription("Get Categories with Pagination");
        }
    }
}
