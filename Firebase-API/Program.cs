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

            // Adicionar configura��o do Firebase
            builder.Services.AddSingleton<FirebaseContext>(provider =>
                new FirebaseContext("https://hobbieshub-api-default-rtdb.firebaseio.com/"));

            // Registrar o FirebaseClient para ser usado pelos reposit�rios
            builder.Services.AddSingleton(provider =>
                provider.GetRequiredService<FirebaseContext>().Client);

            // Registrar o reposit�rio de usu�rios e outras depend�ncias
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Add services to the container.
            builder.Services.AddControllers();
            // Configura��o do Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure o pipeline de requisi��o HTTP.
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
