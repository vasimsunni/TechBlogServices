namespace Blog.Application.Features.Article.Command.DeleteArticle
{
    public record DeleteArticleCommand(Guid ArticleId)
      : ICommand<DeleteArticleResult>;

    public record DeleteArticleResult(bool IsSuccess);

    public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator()
        {
            RuleFor(x => x.ArticleId).NotEmpty().WithMessage("ArticleId is required");
        }
    }
}
