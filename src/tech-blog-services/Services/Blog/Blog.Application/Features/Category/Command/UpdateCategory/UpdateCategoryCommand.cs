namespace Blog.Application.Features.Category.Command.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string CategoryName)
        : ICommand<UpdateCategoryResult>;

    public record UpdateCategoryResult(bool IsSuccess);

    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Name is required");
        }
    }
}
