namespace Blog.Application.Features.Category.Command.DeleteCategory
{
    public class DeleteCategoryCommandHandler
        (IApplicationDbContext dbContext)
        : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResult>
    {
        public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            //Get category from DB
            var categoryId = CategoryId.Of(command.CategoryId);
            var category = await dbContext.Categories.FindAsync([categoryId], cancellationToken);
            if (category is null)
            {
                throw new CategoryNotFoundException(command.CategoryId);
            }

            //Delete category entity from command object and save the changes
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new DeleteCategoryResult(true);
        }
    }
}
