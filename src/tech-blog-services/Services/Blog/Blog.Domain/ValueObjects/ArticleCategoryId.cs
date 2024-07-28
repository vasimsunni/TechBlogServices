namespace Blog.Domain.ValueObjects
{
    public record ArticleCategoryId
    {
        public Guid Value { get; }
        private ArticleCategoryId(Guid value) => Value = value;

        public static ArticleCategoryId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("ArticleCategoryId cannot be empty.");
            }

            return new ArticleCategoryId(value);
        }
    }
}
