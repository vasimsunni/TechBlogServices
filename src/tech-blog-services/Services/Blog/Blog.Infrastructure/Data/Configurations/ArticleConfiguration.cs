namespace Blog.Infrastructure.Data.Configurations
{
    public class ArticleConfiguration
        : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(
                ArticleId => ArticleId.Value,
                dbId => ArticleId.Of(dbId));

            builder.HasMany(a => a.ArticleCategories)
                .WithOne()
                .HasForeignKey(ac => ac.ArticleId);

            builder.Property(c => c.Title).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Body).IsRequired();
        }
    }
}
