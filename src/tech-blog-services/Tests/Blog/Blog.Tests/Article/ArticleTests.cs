using Blog.Infrastructure.Data.Extensions;
using Newtonsoft.Json;
using System.Text;
using Xunit.Extensions.Ordering;

namespace Blog.IntegrationTests.Article
{
    [Order(3)]
    public class ArticleTests : BaseTest
    {
        static string adminUserToken = string.Empty;
        static string bloggerUserToken = string.Empty;
        public ArticleTests(AppBuilder<Program> factory) : base(factory)
        {
            
        }

        [Fact, Order(1)]
        public async Task GetArticles()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/articles/");
            request.Headers.Add("Authorization", "Bearer " + await GenerateBloggerToken());
            var response = await this.Client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<object>(stringResponse);
                var statusCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Fact, Order(2)]
        public async Task GetArticlesByAdmin()
        {
            string adminUserId = InitialData.Users.FirstOrDefault(x => x.UserRole == Domain.Enums.UserRole.Admin)!.Id.Value.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, "/articles/user/" + adminUserId);
            request.Headers.Add("Authorization", "Bearer " + await GenerateAdminToken());
            var response = await this.Client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<object>(stringResponse);
                var statusCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Fact, Order(3)]
        public async Task GetArticlesByBlogger()
        {
            string bloggerUserId = InitialData.Users.FirstOrDefault(x => x.UserRole == Domain.Enums.UserRole.Blogger)!.Id.Value.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, "/articles/user/" + bloggerUserId);
            request.Headers.Add("Authorization", "Bearer " + await GenerateBloggerToken());
            var response = await this.Client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<object>(stringResponse);
                var statusCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Fact, Order(4)]
        public async Task GetArticlesByCategory()
        {
            string categoryId = InitialData.ArticlesWithCategories.FirstOrDefault()!.ArticleCategories.FirstOrDefault()!.Id.Value.ToString();
            var request = new HttpRequestMessage(HttpMethod.Get, "/articles/category/" + categoryId);
            request.Headers.Add("Authorization", "Bearer " + await GenerateBloggerToken());
            var response = await this.Client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<object>(stringResponse);
                var statusCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Fact,Order(5)]
        public async Task CreateArticle()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/articles");
            request.Headers.Add("Authorization", "Bearer " + await GenerateBloggerToken());
            var jsonBody = JsonConvert.SerializeObject(TestFeedData.Article);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await this.Client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ArticleCreatedResponse>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            TestFeedData.Article.Id = new Guid(result.id);
        }

        [Fact, Order(6)]
        public async Task UpdateArticle()
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "/articles");
            request.Headers.Add("Authorization", "Bearer " + await GenerateBloggerToken());
            var jsonBody = JsonConvert.SerializeObject(TestFeedData.Article);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await this.Client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var statusCode = response.StatusCode.ToString();
        }

        [Fact, Order(7)]
        public async Task DeleteArticle()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/articles/"+ TestFeedData.Article.Id);
            request.Headers.Add("Authorization", "Bearer " + await GenerateBloggerToken());
            var response = await this.Client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var statusCode = response.StatusCode.ToString();
        }

        public async Task<string> GenerateAdminToken()
        {
            if (adminUserToken == string.Empty)
            {
                var adminUser = InitialData.Users.FirstOrDefault(x => x.UserRole == Domain.Enums.UserRole.Admin);

                var request = new HttpRequestMessage(HttpMethod.Post, "/users/login");
                var jsonBody = JsonConvert.SerializeObject(new { Email = adminUser.Email, Password = "vasim#123" });
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await this.Client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginResponse>(stringResponse);
                var statusCode = response.StatusCode.ToString();

                adminUserToken = result.token.ToString();
            }

            return adminUserToken;
        }

        private async Task<string> GenerateBloggerToken()
        {
            if (bloggerUserToken == string.Empty)
            {
                var bloggerUser = InitialData.Users.FirstOrDefault(x => x.UserRole == Domain.Enums.UserRole.Blogger);

                var request = new HttpRequestMessage(HttpMethod.Post, "/users/login");
                var jsonBody = JsonConvert.SerializeObject(new { Email = bloggerUser.Email, Password = "vasim#123" });
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await this.Client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var stringResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginResponse>(stringResponse);
                var statusCode = response.StatusCode.ToString();

                bloggerUserToken = result.token.ToString();
            }

            return bloggerUserToken;
        }
    }

    public class LoginResponse()
    {
        public bool isSuccess { get; set; }
        public string token { get; set; }
    }

    public class ArticleCreatedResponse()
    {
        public string id { get; set; }
    }
}
