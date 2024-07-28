namespace Blog.Application.Features.Article.Command.UpdateArticle
{
    public class UpdateArticleCommandHandler
        (IApplicationDbContext dbContext, IUserContext userContext)
        : ICommandHandler<UpdateArticleCommand, UpdateArticleResult>
    {
        public async Task<UpdateArticleResult> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
        {
            //Get Article from DB
            var articleId = ArticleId.Of(command.Id);
            var article = await dbContext.Articles.Include(a => a.ArticleCategories).FirstOrDefaultAsync(a => a.Id == articleId);
            
            if (article == null)
            {
                throw new ArticleNotFoundException(command.Id);
            }

            var user = userContext.GetUser();
            if (article.CreatedBy != user.UserId && user.UserRole != nameof(UserRole.Admin))
            {
                throw new UnauthorizedAccessException("You are not authorized to perform this task");
            }

            UpdateArticleWithNewValues(article, command);

            //save to database
            dbContext.Articles.Update(article);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new UpdateArticleResult(true);
        }

        private void UpdateArticleWithNewValues(Domain.Models.Article article, UpdateArticleCommand articleCommand)
        {
            article.Update(
            title: articleCommand.Title,
               body: articleCommand.Body,
               articleStatus: articleCommand.Status);

            var removedArticleCategories = article.ArticleCategories
                .Where(x => !articleCommand.Categories.Any(ac => ac == x.CategoryId.Value));
            var addedArticleCategories = articleCommand.Categories
                .Where(x => !article.ArticleCategories.Any(ac => ac.CategoryId.Value == x));

            foreach (var category in removedArticleCategories)
            {
                article.RemoveCategory(CategoryId.Of(category.CategoryId.Value));
            }

            foreach (var category in addedArticleCategories)
            {
                article.AddCategory(CategoryId.Of(category));
            }
        }
    }
}
