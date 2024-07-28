namespace Blog.Domain.ValueObjects
{
    public record ArticleId
    {
        public Guid Value { get; }
        private ArticleId(Guid value) => Value = value;

        public static ArticleId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("ArticleId cannot be empty.");
            }

            return new ArticleId(value);
        }
    }
}
