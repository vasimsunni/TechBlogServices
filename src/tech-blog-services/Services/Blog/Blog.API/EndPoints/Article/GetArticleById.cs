using Blog.Application.Features.Article.Query.GetArticleById;

namespace Blog.API.EndPoints.Article
{
    //public record GetArticleByIdRequest(Guid ArticleId);
    public record GetArticleByIdResponse(ArticleDto ArticleDto);
    public class GetArticleById : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("articles/{ArticleId}", async (Guid ArticleId, ISender sender) =>
            {
                var result = await sender.Send(new GetArticleByIdQuery(ArticleId));

                var response = result.Adapt<GetArticleByIdResponse>();

                if (response.ArticleDto == null)
                    return Results.NoContent();

                return Results.Ok(response);
            })
            .RequireAuthorization("AdminAndBlogger")
            .AddEndpointFilter<UserSetterFilter>()
             .WithName("GetArticlesByArticle")
             .Produces<GetArticleByIdResponse>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status204NoContent)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Articles By Article")
             .WithDescription("Get Articles By Article");
        }
    }
}
