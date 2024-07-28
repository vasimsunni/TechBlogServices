namespace Blog.Application.Extensions
{
    public static class CategoryExtentions
    {
        public static IEnumerable<CategoryDto> ToCategoryDtoList(this List<Domain.Models.Category> categories)
        {
            return categories.Select(category => new CategoryDto(
                Id: category.Id.Value,
                Name: category.Name
                ));
        }
    }
}
