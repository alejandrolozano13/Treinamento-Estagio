
using Domain.BancoDeDados;
using Domain.Validacao;
using Infra.Repositorio;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Azure.Management.Storage.Fluent.Models;
using Microsoft.Extensions.FileProviders;

namespace SistemaCadastroWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IRepositorio, RepositorioLinq>();
            builder.Services.AddScoped<ValidadorDeCliente>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseFileServer();
            app.UseDefaultFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
                ),
                ContentTypeProvider = new FileExtensionContentTypeProvider
                {
                    Mappings = { [".properties"] = "application/x-msdownload" }
                }
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            app.MapControllers();

            app.Run();
        }
    }
}