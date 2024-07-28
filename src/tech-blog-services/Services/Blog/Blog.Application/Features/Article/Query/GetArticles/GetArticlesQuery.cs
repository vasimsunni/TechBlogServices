namespace Blog.Application.Features.Article.Query.GetArticles
{
    public record GetArticlesQuery(PaginationRequest PaginationRequest)
       : IQuery<GetArticlesResult>;

    public record GetArticlesResult(PaginatedResult<ArticleDto> Articles);
}
