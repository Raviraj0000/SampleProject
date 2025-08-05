
using Amazon.CloudWatchLogs;
using EmployeeReadService.Persistent;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.AwsCloudWatch;
using EmployeeReadService.Interfaces;
using EmployeeReadService.Logger;
using Microsoft.EntityFrameworkCore;


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

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

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


            //app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.Run();
        }
    }
}
