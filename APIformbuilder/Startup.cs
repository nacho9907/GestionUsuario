using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using IdentityServer4.Configuration;
using IdentityServer4.Validation;
using APIformbuilder;


namespace APIformbuilder
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // Este método se llama en tiempo de ejecución. Use este método para configurar servicios.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:8081")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });


            // Configurar la autenticación JWT
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //{
            //  options.TokenValidationParameters = new TokenValidationParameters
            //{
            // Validar el emisor (issuer) del token
            //  ValidateIssuer = true,
            //  ValidIssuer = "tu_issuer", // Reemplaza con el valor real

            // Validar el receptor (audience) del token
            // ValidateAudience = true,
            // ValidAudience = "tu_audience", // Reemplaza con el valor real

            // Validar el tiempo de vida del token
            // ValidateLifetime = true,

            // Validar la firma del token
            //  ValidateIssuerSigningKey = true,
            //  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tu_clave_secreta")) // Reemplaza con la clave real
            //  };
            // });
        }

        // Este método se llama en tiempo de ejecución. Use este método para configurar el pipeline de solicitud HTTP.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    app.UseCors();
          //  app.UseIdentityServer();
        //}
    }
}

