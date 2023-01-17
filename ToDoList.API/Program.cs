using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;
using Microsoft.OpenApi.Models;
using ToDoList.API.Core.Commands.InsertTodoItem;
using ToDoList.API.Core.Interfaces;
using ToDoList.API.Core.Interfaces.Repositories;
using ToDoList.API.Core.Interfaces.Services;
using ToDoList.API.Core.Services;
using ToDoList.API.Infrastructure.Auth;
using ToDoList.API.Infrastructure.Data.EntityFramework;
using ToDoList.API.Infrastructure.Data.EntityFramework.Repositories;

namespace ToDoList.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddMediatR(typeof(InsertTodoItemCommandHandler).Assembly);

            builder.Services.AddDbContext<ToDoItemsContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));
            
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            builder.Services.AddScoped<IUserLoginService, UserLoginService>();
            builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
            builder.Services.AddScoped<ITodoItemsService, TodoItemsService>();
            
            builder.Services.AddScoped<IJwtHandler, JwtHandler>();
            
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITodoItemsRepository, TodoItemsRepository>();

            builder.Services.ConfigureAutoMapper();

            builder.Services.ConfigureIdentity();

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;   // set to true

                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"]))
                };
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "WebAPI",
                    Description = "Todo List"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme 
                        {
                            Reference = new OpenApiReference 
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/V1/swagger.json", "Todo List WebAPI");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}