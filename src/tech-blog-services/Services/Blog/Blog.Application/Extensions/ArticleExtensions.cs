namespace Blog.Application.Extensions
{
    public static class ArticleExtensions
    {
        public static IEnumerable<ArticleDto> ToArticleDtoList(this List<Domain.Models.Article> articles, List<Domain.Models.Category> categories, List<Domain.Models.User> users)
        {
            return articles.Select(article => new ArticleDto(
                Id: article.Id.Value,
                Title: article.Title,
                Body: article.Body,
                ArticleStatus: Enum.GetName((ArticleStatus)article.ArticleStatus)!,
                CreatedBy: users.Where(x => x.Id.Value.ToString() == article.CreatedBy).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault()!,
                LastModifiedOn: article.LastModified.HasValue ? article.LastModified.Value.ToString("dd/MM/yyyy HH:mm") : article.CreatedAt?.ToString("dd/MM/yyyy HH:mm")!,
                ArticleCategories: article.ArticleCategories
                                    .Select(ac => new CategoryDto(Id: ac.CategoryId.Value, Name: categories.FirstOrDefault(x => x.Id == ac.CategoryId)?.Name!))
                                    .ToList()
                ));
        }

        public static ArticleDto ToArticleDto(this Domain.Models.Article article, List<Domain.Models.Category> categories, Domain.Models.User user)
        {
            return new ArticleDto(
                Id: article.Id.Value,
                Title: article.Title,
                Body: article.Body,
                ArticleStatus: Enum.GetName((ArticleStatus)article.ArticleStatus)!,
                CreatedBy: user != null ? user.FirstName + " " + user.LastName : "",
                LastModifiedOn: article.LastModified.HasValue ? article.LastModified.Value.ToString("dd/MM/yyyy HH:mm") : article.CreatedAt?.ToString("dd/MM/yyyy HH:mm")!,
                ArticleCategories: article.ArticleCategories
                                    .Select(ac => new CategoryDto(Id: ac.CategoryId.Value, Name: categories.FirstOrDefault(x => x.Id == ac.CategoryId)?.Name!))
                                    .ToList()
                );
        }

    }
}
