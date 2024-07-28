namespace Blog.Infrastructure.Data.Configurations
{
    public class UserConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(
                UserId => UserId.Value,
                dbId => UserId.Of(dbId));

            builder.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
            builder.Property(c => c.LastName).HasMaxLength(100);

            builder.Property(c => c.Email).HasMaxLength(255);
            builder.HasIndex(c => c.Email).IsUnique();

            builder.Property(c => c.Password).IsRequired();

            builder.Property(c => c.UserRole).IsRequired();
        }
    }
}
