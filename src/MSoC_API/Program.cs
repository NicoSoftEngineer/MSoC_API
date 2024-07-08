
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Configuration;
using MSoC_API.Services;
using MSoC_API.Utils;
using System.Net.Mail;
using System.Net;

namespace MSoC_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<FileService>();

            builder.Services.AddSingleton<FileService>();

            var fileSystemOptions = new FileSystemOptions();

            builder.Configuration.GetSection("FileSystemOptions").Bind(fileSystemOptions);

            builder.Services.AddTransient(_ => fileSystemOptions);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);


            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
