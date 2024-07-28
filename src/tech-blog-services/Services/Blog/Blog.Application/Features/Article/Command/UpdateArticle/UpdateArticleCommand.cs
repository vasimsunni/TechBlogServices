namespace Blog.Application.Features.Article.Command.UpdateArticle
{
    public record UpdateArticleCommand(Guid Id, string Title, string Body, string Status, List<Guid> Categories)
        : ICommand<UpdateArticleResult>;

    public record UpdateArticleResult(bool IsSuccess);

    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Body is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
            RuleFor(x => x.Categories).NotNull().NotEmpty().WithMessage("Atleast one Category is required");
        }
    }
}
