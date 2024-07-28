namespace Blog.Application.Dtos
{
    public record ArticleDto(Guid Id,
                        string Title,
                        string Body,
                        string ArticleStatus,
                        string CreatedBy,
                        string LastModifiedOn,
                        List<CategoryDto> ArticleCategories);
}
