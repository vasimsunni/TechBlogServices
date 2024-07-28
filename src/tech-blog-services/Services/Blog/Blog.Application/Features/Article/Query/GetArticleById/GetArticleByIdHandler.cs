namespace Blog.Application.Features.Article.Query.GetArticleById
{
    public class GetArticleByIdQueryHandler
         (IApplicationDbContext dbContext)
        : IQueryHandler<GetArticleByIdQuery, GetArticleByIdResult>
    {
        public async Task<GetArticleByIdResult> Handle(GetArticleByIdQuery query, CancellationToken cancellationToken)
        {
            //Get article using article id
            var article = await dbContext.Articles
                    .Include(a => a.ArticleCategories)
                    .FirstOrDefaultAsync(x => x.Id == ArticleId.Of(query.ArticleId), cancellationToken);

            if (article is null)
            {
                return new GetArticleByIdResult(null);
            }

            //get all category ids from article
            var categoryIds = article.ArticleCategories?.Select(c => c.CategoryId);

            //get categories using category ids
            var categories = await dbContext.Categories.Join(categoryIds, c => c.Id, id => id, (c, id) => c).ToListAsync();

            var userId = UserId.Of(new Guid(article.CreatedBy!));

            var user = await dbContext.Users.FindAsync([userId], cancellationToken);

            // return result
            return new GetArticleByIdResult(article.ToArticleDto(categories, user));
        }
    }
}
