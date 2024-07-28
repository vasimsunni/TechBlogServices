using Blog.API;
using Blog.Application;
using Blog.Infrastructure;
using Blog.Infrastructure.Data.Extensions;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services
             .AddApplicationServices()
             .AddInfrastructureServices(builder.Configuration)
             .AddApiServices(builder.Configuration);

        var app = builder.Build();

        //Configure the HTTP request pipeline
        app.UseApiServices();

        if (app.Environment.IsDevelopment())
        {
            await app.InitialiseDatabaseAsync();
        }

        app.Run();
    }
}