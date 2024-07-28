namespace Blog.Application.Features.Article.Query.GetArticlesByUser
{
    public record GetArticlesByUserQuery(Guid UserId)
       : IQuery<GetArticlesByUserResult>;

    public record GetArticlesByUserResult(IEnumerable<ArticleDto> Articles);
}
