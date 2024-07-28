namespace Blog.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration
        : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(
                CategoryId => CategoryId.Value,
                dbId => CategoryId.Of(dbId));

            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        }
    }
}
