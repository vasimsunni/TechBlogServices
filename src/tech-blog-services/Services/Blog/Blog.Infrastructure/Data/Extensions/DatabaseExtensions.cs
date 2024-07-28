using Blog.Application.Helpers;
using Microsoft.AspNetCore.Builder;

namespace Blog.Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context, userContext);
        }

        public static async Task InitialiseDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context, userContext);
        }

        private static async Task SeedAsync(ApplicationDbContext context, IUserContext userContext)
        {
            var adminUser = InitialData.Users.FirstOrDefault(x => x.UserRole == Domain.Enums.UserRole.Admin);
            var bloggerUser = InitialData.Users.FirstOrDefault(x => x.UserRole == Domain.Enums.UserRole.Blogger);

            userContext.SetUser(adminUser?.Id.Value.ToString()!, "Admin");
            await SeedUserAsync(context);
            await SeedCategoryAsync(context);

            userContext.SetUser(bloggerUser?.Id.Value.ToString()!, "Blogger");
            await SeedArticleAsync(context);

            userContext.SetUser("", "");
        }
        private static async Task SeedUserAsync(ApplicationDbContext context)
        {
            if (!await context.Users.AnyAsync())
            {
                await context.Users.AddRangeAsync(InitialData.Users);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedCategoryAsync(ApplicationDbContext context)
        {
            if (!await context.Categories.AnyAsync())
            {
                await context.Categories.AddRangeAsync(InitialData.Categories);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedArticleAsync(ApplicationDbContext context)
        {
            if (!await context.Articles.AnyAsync())
            {
                await context.Articles.AddRangeAsync(InitialData.ArticlesWithCategories);
                await context.SaveChangesAsync();
            }
        }
    }
}
