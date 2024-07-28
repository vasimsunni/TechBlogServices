using Blog.Application.Features.Article.Command.DeleteArticle;

namespace Blog.API.EndPoints.Article
{
    //Accepts the ArticleId from parameter
    //Constructs a DeleteArticleCommand
    //Sends the command using MediatR
    //Returns a success or not found response

    //public record DeleteArticleRequest(Guid ArticleId);
    public record DeleteArticleResponse(bool IsSuccess);
    public class DeleteArticle : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/articles/{ArticleId}", async (Guid ArticleId, ISender sender) =>
            {
                var result = await sender.Send(new DeleteArticleCommand(ArticleId));

                var response = result.Adapt<DeleteArticleResponse>();

                return Results.Ok(response);
            })
            .RequireAuthorization("AdminAndBlogger")
            .AddEndpointFilter<UserSetterFilter>()
            .WithName("DeleteArticle")
            .Produces<DeleteArticleResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Article")
            .WithDescription("Delete Article");

        }
    }
}
