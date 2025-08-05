
using Amazon.CloudWatchLogs;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using EmployeeWriteService.Interfaces;
using EmployeeWriteService.Logger;
using EmployeeWriteService.Persistence;
using EmployeeWriteService.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.AwsCloudWatch;


namespace EmployeeWriteService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cloudWatchClient = new AmazonCloudWatchLogsClient();
            var options = new CloudWatchSinkOptions
            {
                LogGroupName = "your-log-group",
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

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<WriteDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WriteDb")));

            builder.Services.AddAWSService<IAmazonS3>();
            builder.Services.Configure<AWSOptions>(builder.Configuration.GetSection("AWS"));
            builder.Services.AddScoped<IS3Uploader, S3Uploader>();

            builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(SerilogLogger<>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization(); 
            

            app.Run();
        }
    }
}
