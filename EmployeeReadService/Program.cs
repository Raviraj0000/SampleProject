
using Amazon.CloudWatchLogs;
using EmployeeReadService.Interfaces;
using EmployeeReadService.Logger;
using EmployeeReadService.Persistent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.AwsCloudWatch;
using System;
using System.Reflection;


namespace EmployeeReadService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var cloudWatchClient = new AmazonCloudWatchLogsClient();
            var options = new CloudWatchSinkOptions
            {
                LogGroupName = "my-all-logs",
                LogStreamNameProvider = new DefaultLogStreamProvider(),
                TextFormatter = new JsonFormatter(),
                CreateLogGroup = true,
                MinimumLogEventLevel = LogEventLevel.Information
            };

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("Logs/app-log.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.AmazonCloudWatch(options, cloudWatchClient)
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);                       
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            builder.Services.AddControllers();


            // Add services to the container.
            //builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            //builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ReadDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ReadDb")));

            builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(SerilogLogger<>));

            var config = builder.Configuration;
            var awsOptions = config.GetAWSOptions();
            builder.Services.AddDefaultAWSOptions(awsOptions);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.MapOpenApi();
            //}

            app.UseRouting();
            app.MapGet("/", () => "Employee Read Service is running");           

            //app.UseHttpsRedirection();

            app.UseAuthorization();
         
            app.MapControllers();

            app.Run();
        }
    }
}
