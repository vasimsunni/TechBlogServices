namespace Blog.IntegrationTests
{
    public class BaseTest : IClassFixture<AppBuilder<Program>>
    {
        private readonly AppBuilder<Program> _factory;
        public readonly HttpClient Client;

        public BaseTest(AppBuilder<Program> factory)
        {
            _factory = factory;
            Client = _factory.CreateClient();
        }
    }
}
