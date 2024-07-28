using Blog.Application.Features.Article.Command.UpdateArticle;

namespace Blog.API.EndPoints.Article
{
    //Accepts a UpdateArticleRequest object
    //Maps the request to a UpdateArticleCommand handler
    //Uses MediatR to send the command to the corresponding handler
    //Returns a success or error response based on the outcome

    public record UpdateArticleRequest(Guid Id, string Title, string Body, string Status, List<Guid> Categories);
    public record UpdateArticleResponse(bool IsSuccess);
    public class UpdateArticle : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/Articles", async (UpdateArticleRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateArticleCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateArticleResponse>();

                return Results.Ok(response);
            })
           .RequireAuthorization("AdminAndBlogger")
            .AddEndpointFilter<UserSetterFilter>()
           .WithName("UpdateArticle")
           .Produces<UpdateArticleResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .WithSummary("Update Article")
           .WithDescription("Update Article");
        }
    }
}
