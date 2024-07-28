using Blog.Application.Features.Article.Command.CreateArticle;

namespace Blog.API.EndPoints.Article
{
    //Accepts a CreateArticleRequest object
    //Maps the request to a CreateArticleCommand handler
    //Uses MediatR to send the command to the corresponding handler
    //Returns a response with the created Article's Id

    public record CreateArticleRequest(string Title, string Body, List<Guid> Categories);
    public record CreateArticleResponse(Guid Id);
    public class CreateArticle : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/articles", async (CreateArticleRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateArticleCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateArticleResponse>();

                return Results.Created($"/articles/{response.Id}", response);
            })
            .RequireAuthorization("AdminAndBlogger")
            .AddEndpointFilter<UserSetterFilter>()
            .WithName("CreateArticle")
            .Produces<CreateArticleResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Article")
            .WithDescription("Create Article");
        }
    }
}
