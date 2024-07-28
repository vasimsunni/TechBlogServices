namespace Blog.Application.Features.Category.Command.CreateCategory
{
    public record CreateCategoryCommand(string CategoryName)
        : ICommand<CreateCategoryResult>;

    public record CreateCategoryResult(Guid Id);

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Name is required");
        }
    }
}
