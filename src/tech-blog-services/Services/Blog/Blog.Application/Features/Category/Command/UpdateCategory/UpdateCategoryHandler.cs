namespace Blog.Application.Features.Category.Command.UpdateCategory
{
    public class UpdateCategoryCommandHandler
        (IApplicationDbContext dbContext)
        : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResult>
    {
        public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            //Get category from DB
            var categoryId = CategoryId.Of(command.Id);
            var category = await dbContext.Categories.FindAsync([categoryId], cancellationToken);
            if (category is null)
            {
                throw new CategoryNotFoundException(command.Id);
            }

            //Update order entry from command object
            category.Name = command.CategoryName;

            //save to database
            dbContext.Categories.Update(category);
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new UpdateCategoryResult(true);
        }
    }
}
