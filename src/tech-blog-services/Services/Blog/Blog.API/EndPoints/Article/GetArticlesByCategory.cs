
using Blog.Application.Features.Article.Query.GetArticlesByCategory;

namespace Blog.API.EndPoints.Article
{
    //public record GetArticleByCategoryRequest(Guid CategoryId);
    public record GetArticlesByCategoryResponse(IEnumerable<ArticleDto> Articles);
    public class GetArticlesByCategory : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("articles/category/{CategoryId}", async (Guid CategoryId, ISender sender) =>
            {
                var result = await sender.Send(new GetArticlesByCategoryQuery(CategoryId));

                var response = result.Adapt<GetArticlesByCategoryResponse>();

                if (!response.Articles.Any())
                    return Results.NoContent();

                return Results.Ok(response);
            })
             .RequireAuthorization("AdminAndBlogger")
             .AddEndpointFilter<UserSetterFilter>()
             .WithName("GetArticlesByCategory")
             .Produces<GetArticlesByCategoryResponse>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status204NoContent)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Articles By Category")
             .WithDescription("Get Articles By Category");
        }
    }
}
