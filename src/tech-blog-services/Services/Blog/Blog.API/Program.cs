using Blog.API;
using Blog.Application;
using Blog.Infrastructure;
using Blog.Infrastructure.Data.Extensions;
using BuildingBlocks.Logger;
using Serilog;
using Serilog.Sinks.Elasticsearch;

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


        //Register Serilog to Host (used to pass logs to elasticsearch)
        builder.Host.UseSerilog(SeriLogger.Configure);


        var app = builder.Build();

        //Configure the HTTP request pipeline
        app.UseApiServices();

        //User serilog request logging to elasticsearch
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            await app.InitialiseDatabaseAsync();
        }

        app.Run();
    }
}