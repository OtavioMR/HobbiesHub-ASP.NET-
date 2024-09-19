using HobbiesHub_API_REST.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configuração do DbContext
        builder.Services.AddDbContext<HobbiesHubSystemDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        // Add services to the container.
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyAllowsSpecificOrigins",
                policyBuilder =>
                {
                    policyBuilder.WithOrigins("http://127.0.0.1:5501")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("MyAllowsSpecificOrigins");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
