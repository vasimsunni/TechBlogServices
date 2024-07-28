namespace Blog.Application.Features.Article.Command.CreateArticle
{
    public record CreateArticleCommand(string Title, string Body, List<Guid> Categories)
       : ICommand<CreateArticleResult>;

    public record CreateArticleResult(Guid Id);

    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Body is required");
            RuleFor(x => x.Categories).NotNull().NotEmpty().WithMessage("Atleast one Category is required");
        }
    }
}
