namespace Blog.Application.Features.Category.Query.GetCategories
{
    public record GetCategoriesQuery(PaginationRequest PaginationRequest)
        : IQuery<GetCategoriesResult>;

    public record GetCategoriesResult(PaginatedResult<CategoryDto> Categories);
}
