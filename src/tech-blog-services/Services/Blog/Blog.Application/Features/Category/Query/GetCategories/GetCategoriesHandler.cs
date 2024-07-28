
using Blog.Application.Helpers;

namespace Blog.Application.Features.Category.Query.GetCategories
{
    public class GetCategoriesQueryHandler
        (IApplicationDbContext dbContext)
        : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
    {
        public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            // get categories with pagination
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Categories.LongCountAsync(cancellationToken);

            var categories = await dbContext.Categories
                                .OrderBy(o => o.Name)
                                .Skip(pageSize * pageIndex)
                                .Take(pageSize)
                                .ToListAsync(cancellationToken);

            // return result
            return new GetCategoriesResult(
                new PaginatedResult<CategoryDto>(
                   pageIndex,
                   pageSize,
                   totalCount,
                   categories.ToCategoryDtoList()));
        }
    }
}
