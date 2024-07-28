namespace Blog.Application.Features.Article.Query.GetArticlesByCategory
{
    public record GetArticlesByCategoryQuery(Guid CategoryId)
        : IQuery<GetArticlesByCategoryResult>;

    public record GetArticlesByCategoryResult(IEnumerable<ArticleDto> Articles);
}



