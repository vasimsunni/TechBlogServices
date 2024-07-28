namespace Blog.Application.Features.Article.Command.DeleteArticle
{
    public class DeleteArticleCommandHandler
         (IApplicationDbContext dbContext, IUserContext userContext)
        : ICommandHandler<DeleteArticleCommand, DeleteArticleResult>
    {
        public async Task<DeleteArticleResult> Handle(DeleteArticleCommand command, CancellationToken cancellationToken)
        {
            //Get Article from DB
            var articleId = ArticleId.Of(command.ArticleId);
            var article = await dbContext.Articles.FindAsync([articleId], cancellationToken);
            if (article is null)
            {
                throw new ArticleNotFoundException(command.ArticleId);
            }

            var user = userContext.GetUser();
            if (article.CreatedBy != user.UserId && user.UserRole != nameof(UserRole.Admin))
            {
                throw new UnauthorizedAccessException("You are not authorized to perform this task");
            }

            //Delete Article entity from command object and save the changes
            dbContext.Articles.Remove(article);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new DeleteArticleResult(true);
        }
    }
}
