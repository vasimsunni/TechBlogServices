using Blog.Application.Data;

namespace Blog.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext
            (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ArticleCategory> ArticleCategories => Set<ArticleCategory>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
