using Blog.Application.Features.Article.Query.GetArticlesByUser;

namespace Blog.API.EndPoints.Article
{
    //public record GetArticleByUserRequest(Guid UserId);
    public record GetArticleByUserResponse(IEnumerable<ArticleDto> Articles);
    public class GetArticlesByUser : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("articles/user/{UserId}", async (Guid UserId, ISender sender) =>
            {
                var result = await sender.Send(new GetArticlesByUserQuery(UserId));

                var response = result.Adapt<GetArticleByUserResponse>();

                if (!response.Articles.Any())
                    return Results.NoContent();

                return Results.Ok(response);
            })
             .RequireAuthorization("AdminAndBlogger")
            .AddEndpointFilter<UserSetterFilter>()
             .WithName("GetArticlesByUser")
             .Produces<GetArticleByUserResponse>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status204NoContent)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("Get Articles By User")
             .WithDescription("Get Articles By User");
        }
    }
}
