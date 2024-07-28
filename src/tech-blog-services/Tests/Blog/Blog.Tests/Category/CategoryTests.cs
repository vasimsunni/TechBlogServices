using Blog.Infrastructure.Data.Extensions;
using Blog.IntegrationTests.Article;
using Newtonsoft.Json;
using System.Text;
using Xunit.Extensions.Ordering;

namespace Blog.IntegrationTests.Category
{
    [Order(2)]
    public class CategoryTests : BaseTest
    {
        static string adminUserToken = string.Empty;

        public CategoryTests(AppBuilder<Program> factory) : base(factory)
        {
            
        }

        [Fact, Order(1)]
        public async Task GetCategories()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/categories");
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


        [Fact, Order(2)]
        public async Task CreateCategory()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/categories");
            request.Headers.Add("Authorization", "Bearer " + await GenerateAdminToken());
            var jsonBody = JsonConvert.SerializeObject(TestFeedData.Category);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await this.Client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CategoryCreatedResponse>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            TestFeedData.Category.Id = new Guid(result.id);
        }

        [Fact,Order(3)]
        public async Task UpdateCatgeory()
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "/categories");
            request.Headers.Add("Authorization", "Bearer " + await GenerateAdminToken());
            var jsonBody = JsonConvert.SerializeObject(TestFeedData.Category);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await this.Client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var statusCode = response.StatusCode.ToString();
        }

        [Fact, Order(4)]
        public async Task DeleteCategory()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/categories/"+ TestFeedData.Category.Id);
            request.Headers.Add("Authorization", "Bearer " + await GenerateAdminToken());
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
    }

    public class CategoryCreatedResponse()
    {
        public string id { get; set; }
    }
}
