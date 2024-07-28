
using Microsoft.EntityFrameworkCore.Storage;

namespace Blog.Application.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Article> Articles { get; }
        public DbSet<Category> Categories { get; }
        public DbSet<ArticleCategory> ArticleCategories { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
