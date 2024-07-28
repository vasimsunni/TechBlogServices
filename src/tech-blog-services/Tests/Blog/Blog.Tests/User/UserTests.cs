using Blog.Infrastructure.Data.Extensions;
using Newtonsoft.Json;
using System.Text;
using Xunit.Extensions.Ordering;

namespace Blog.IntegrationTests.User
{
    [Order(1)]
    public class UserTests : BaseTest
    {
        
        public UserTests(AppBuilder<Program> factory) : base(factory)
        {
            
        }

        [Fact, Order(1)]
        public async Task LoginShouldSucceedWithCorrectCredentails()
        {
            var adminUser = InitialData.Users.FirstOrDefault(x => x.UserRole == Domain.Enums.UserRole.Admin);

            var request = new HttpRequestMessage(HttpMethod.Post, "/users/login");
            var jsonBody = JsonConvert.SerializeObject(new { Email = adminUser.Email, Password = "vasim#123" });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await this.Client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<object>(stringResponse);
            var statusCode = response.StatusCode.ToString();
        }

        [Fact, Order(2)]
        public async Task LoginShouldFailWithInCorrectCredentails()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/users/login");
            var jsonBody = JsonConvert.SerializeObject(new { Email = "testuser@test.com", Password = "test123" });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await this.Client.SendAsync(request);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<object>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("NotFound", statusCode);
        }

        [Fact, Order(3)]
        public async Task RegisterBloggerWithCorrectDetails()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/users/register/blogger");
            var jsonBody = JsonConvert.SerializeObject(new { FirstName = "Test", LastName = "User", Email = RandomString(5) + "@test.com", Password = "safer#123" });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await this.Client.SendAsync(request);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<object>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("Created", statusCode);
        }

        [Fact, Order(4)]
        public async Task RegisterBloggerFailWithNoDetails()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/users/register/blogger");
            var jsonBody = JsonConvert.SerializeObject(new { });
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await this.Client.SendAsync(request);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<object>(stringResponse);
            var statusCode = response.StatusCode.ToString();

            Assert.Equal("BadRequest", statusCode);
        }


        private static string RandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
