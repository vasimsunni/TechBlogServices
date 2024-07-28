namespace Blog.Application.Features.Category.Command.CreateCategory
{
    public class CreateCategoryCommandHandler
        (IApplicationDbContext dbContext)
        : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
    {
        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            //Create Category Entity
            var category = Domain.Models.Category.Create(id: CategoryId.Of(Guid.NewGuid()),
                                   name: command.CategoryName);

            //Save to database
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateCategoryResult(category.Id.Value);
        }
    }
}
