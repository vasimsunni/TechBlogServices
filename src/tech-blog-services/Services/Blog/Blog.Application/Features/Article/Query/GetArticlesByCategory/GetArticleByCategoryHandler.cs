
namespace Blog.Application.Features.Article.Query.GetArticlesByCategory
{
    public class GetArticleByCategoryQueryHandler
         (IApplicationDbContext dbContext)
        : IQueryHandler<GetArticlesByCategoryQuery, GetArticlesByCategoryResult>
    {
        public async Task<GetArticlesByCategoryResult> Handle(GetArticlesByCategoryQuery query, CancellationToken cancellationToken)
        {
            // get article ids from ArticleCategories using db context
            var articleIds = await dbContext.ArticleCategories
                                .AsNoTracking()
                                .Where(x => x.CategoryId == CategoryId.Of(query.CategoryId))
                                .Select(ac => ac.ArticleId)
                                .ToListAsync();

            //Get articles using article ids
            var articles = await dbContext.Articles
                    .Join(articleIds, a => a.Id, id => id, (a, id) => a)
                    .Include(a => a.ArticleCategories)
                    .AsNoTracking()
                    .OrderBy(o => o.CreatedAt)
                    .ToListAsync(cancellationToken);

            //get all category ids from article
            var categoryIds = articles.SelectMany(x => x.ArticleCategories.Select(c => c.CategoryId)).Distinct().ToList();  

            //get categories using category ids
            var categories = await dbContext.Categories.Join(categoryIds, c => c.Id, id => id, (c, id) => c).ToListAsync();

            //get all user ids from article
            var userIds = articles.Select(x => x.CreatedBy)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .Select(x => UserId.Of(new Guid(x)))
                .ToList();

            //get users using user ids
            var users = await dbContext.Users.Join(userIds, u => u.Id, id => id, (u, id) => u).ToListAsync(cancellationToken);

            // return result
            return new GetArticlesByCategoryResult(articles.ToArticleDtoList(categories, users));
        }
    }
}
