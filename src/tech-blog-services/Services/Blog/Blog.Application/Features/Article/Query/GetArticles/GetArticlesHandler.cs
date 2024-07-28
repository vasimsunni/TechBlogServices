namespace Blog.Application.Features.Article.Query.GetArticles
{
    public class GetArticlesCommandHandler
        (IApplicationDbContext dbContext)
        : IQueryHandler<GetArticlesQuery, GetArticlesResult>
    {
        public async Task<GetArticlesResult> Handle(GetArticlesQuery query, CancellationToken cancellationToken)
        {
            // get Articles with pagination
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Articles.LongCountAsync(cancellationToken);

            var articles = await dbContext.Articles
                                .Include(a=>a.ArticleCategories)
                                .OrderBy(a => a.CreatedAt)
                                .Skip(pageSize * pageIndex)
                                .Take(pageSize)
                                .ToListAsync(cancellationToken);

            //get all category ids from article
            var categoryIds = articles.SelectMany(x => x.ArticleCategories.Select(c => c.CategoryId)).Distinct().ToList();

            //get categories using category ids
            var categories = await dbContext.Categories.Join(categoryIds, c => c.Id, id => id, (c, id) => c).ToListAsync(cancellationToken);

            //get all user ids from article
            var userIds = articles.Select(x => x.CreatedBy)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .Select(x => UserId.Of(new Guid(x)))
                .ToList();

            //get users using user ids
            var users = await dbContext.Users.Join(userIds, u => u.Id, id => id, (u, id) => u).ToListAsync(cancellationToken);

            // return result
            return new GetArticlesResult(
                new PaginatedResult<ArticleDto>(
                   pageIndex,
                   pageSize,
                   totalCount,
                   articles.ToArticleDtoList(categories, users)));
        }

        
    }
}
