namespace Blog.Application.Features.Article.Query.GetArticleById
{
    public record GetArticleByIdQuery(Guid ArticleId)
        : IQuery<GetArticleByIdResult>;

    public record GetArticleByIdResult(ArticleDto ArticleDto);
}
