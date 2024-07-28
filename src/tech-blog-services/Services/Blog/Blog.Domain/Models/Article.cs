namespace Blog.Domain.Models
{
    public class Article : Aggregate<ArticleId>
    {
        public readonly List<ArticleCategory> _articleCategories = new();
        public IReadOnlyList<ArticleCategory> ArticleCategories => _articleCategories.AsReadOnly();

        public string Title { get; private set; } = default!;
        public string Body { get; private set; } = default!;
        public ArticleStatus ArticleStatus { get; private set; } = ArticleStatus.UnPublished;

        public static Article Create(ArticleId id, string title, string body)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title);
            ArgumentException.ThrowIfNullOrWhiteSpace(body);

            var article = new Article
            {
                Id = id,
                Title = title,
                Body = body,
                ArticleStatus = ArticleStatus.UnPublished
            };

            article.AddDomainEvent(new ArticleCreatedEvent(article));

            return article;
        }

        public void Update(string title, string body, string articleStatus)
        {
            Enum.TryParse(articleStatus, out ArticleStatus status);

            Title = title;
            Body = body;
            ArticleStatus = status; 

            AddDomainEvent(new ArticleUpdatedEvent(this));
        }

        public void AddCategory(CategoryId categoryId)
        {
            var articleCategory = new ArticleCategory(Id, categoryId);
            _articleCategories.Add(articleCategory);
        }

        public void RemoveCategory(CategoryId categoryId)
        {
            var articleCategory = _articleCategories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (articleCategory is not null)
            {
                _articleCategories.Remove(articleCategory);
            }
        }
    }
}
