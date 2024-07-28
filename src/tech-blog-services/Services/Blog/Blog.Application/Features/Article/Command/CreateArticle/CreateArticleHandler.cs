namespace Blog.Application.Features.Article.Command.CreateArticle
{
    public class CreateArticleCommandHandler
         (IApplicationDbContext dbContext)
        : ICommandHandler<CreateArticleCommand, CreateArticleResult>
    {
        public async Task<CreateArticleResult> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
        {
            //Create Article Entity
            var article = Domain.Models.Article.Create(id: ArticleId.Of(Guid.NewGuid()),
                                   title: command.Title,
                                   body: command.Body);

            foreach (var category in command.Categories)
            {
                article.AddCategory(categoryId: CategoryId.Of(category));
            }

            //Save to database
            dbContext.Articles.Add(article);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateArticleResult(article.Id.Value);
        }
    }
}
