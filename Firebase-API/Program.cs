using Firebase_API.Data;
using Firebase_API.Models;
using Firebase_API.Repositories;
using Firebase_API.Repositories.Interfaces;

namespace Firebase_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adicionar configuração do Firebase
            builder.Services.AddSingleton<FirebaseContext>(provider =>
                new FirebaseContext("https://hobbieshub-api-default-rtdb.firebaseio.com/"));

            // Registrar o FirebaseClient para ser usado pelos repositórios
            builder.Services.AddSingleton(provider =>
                provider.GetRequiredService<FirebaseContext>().Client);

            // Registrar o repositório de usuários e outras dependências
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Add services to the container.
            builder.Services.AddControllers();
            // Configuração do Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure o pipeline de requisição HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
