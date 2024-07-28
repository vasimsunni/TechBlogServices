using Blog.Application.Features.Article.Query.GetArticles;
using BuildingBlocks.Pagination;

namespace Blog.API.EndPoints.Article
{
    //public record GetArticlesRequest(PaginationRequest PaginationRequest);
    public record GetArticlesResponse(PaginatedResult<ArticleDto> Articles);
    public class GetArticles : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/articles", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetArticlesQuery(request));

                var response = result.Adapt<GetArticlesResponse>();

                return Results.Ok(response);
            })
             .RequireAuthorization("AdminAndBlogger")             
            .AddEndpointFilter<UserSetterFilter>()
             .WithName("GetArticles")
             .Produces<GetArticlesResponse>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Articles with Pagination")
             .WithDescription("Get Articles with Pagination");
        }
    }
}
