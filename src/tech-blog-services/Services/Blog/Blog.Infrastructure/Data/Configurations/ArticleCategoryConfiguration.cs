namespace Blog.Infrastructure.Data.Configurations
{
    public class ArticleCategoryConfiguration
        : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.HasKey(ac => ac.Id);
            builder.Property(ac => ac.Id).HasConversion(
                articleCategoryId => articleCategoryId.Value,
                dbId => ArticleCategoryId.Of(dbId));

            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(ac => ac.CategoryId);

            builder.HasOne<Article>()
                .WithMany()
                .HasForeignKey(ac => ac.ArticleId);
        }
    }
}
