
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechsysLog.Application;
using TechsysLog.Application.Hubs.Orders;
using TechsysLog.Common.Middleware;
using TechsysLog.Infrastructure;
using TechsysLog.Infrastructure.Persistence;

namespace TechsysLog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddRepository(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddWebServices();

            AddJwt(builder);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSignalR();
            

            builder.Services.AddSingleton<MongoDbContext>();

            var app = builder.Build();

           
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.MapHub<OrdersHub>("/ordersHub");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void AddJwt(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
