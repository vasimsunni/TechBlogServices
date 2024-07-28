namespace Blog.Domain.Models
{
    public class ArticleCategory : Entity<ArticleCategoryId>
    {
        internal ArticleCategory(ArticleId articleId, CategoryId categoryId)
        {
            Id = ArticleCategoryId.Of(Guid.NewGuid());
            ArticleId = articleId;
            CategoryId = categoryId;
        }

        public ArticleId ArticleId { get; private set; } = default!;
        public CategoryId CategoryId { get; private set; } = default!;
    }
}
