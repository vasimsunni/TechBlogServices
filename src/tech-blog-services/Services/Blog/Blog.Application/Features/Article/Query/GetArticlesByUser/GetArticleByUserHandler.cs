using Blog.Domain.Models;

namespace Blog.Application.Features.Article.Query.GetArticlesByUser
{
    public class GetArticleByUserHandler
         (IApplicationDbContext dbContext, IUserContext userContext)
        : IQueryHandler<GetArticlesByUserQuery, GetArticlesByUserResult>
    {
        public async Task<GetArticlesByUserResult> Handle(GetArticlesByUserQuery query, CancellationToken cancellationToken)
        {
            var user = userContext.GetUser();
            if (query.UserId.ToString() != user.UserId && user.UserRole != nameof(UserRole.Admin))
            {
                throw new UnauthorizedAccessException("You are not authorized to perform this task");
            }

            //Get articles using user id
            var articles = await dbContext.Articles
                    .Include(a => a.ArticleCategories)
                    .Where(x => x.CreatedBy == query.UserId.ToString())
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
            return new GetArticlesByUserResult(articles.ToArticleDtoList(categories, users));
        }
    }
}
