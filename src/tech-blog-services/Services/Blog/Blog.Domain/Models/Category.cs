namespace Blog.Domain.Models
{
    public class Category : Entity<CategoryId>
    {
        public string Name { get; set; } = default!;

        public static Category Create(CategoryId id, string name)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            var category = new Category
            {
                Id = id,
                Name = name
            };

            return category;
        }
    }
}
