using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.API.Configuration;
using ToDoList.API.Infrastructure.Data.Entities;
using ToDoList.API.Infrastructure.Data.EntityFramework;

namespace ToDoList.API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ToDoItemsContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            //services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            var mapperConfig = new MapperConfiguration(map =>
            {
                //map.AddProfile<TeacherMappingProfile>();
                //map.AddProfile<StudentMappingProfile>();
                map.AddProfile<AppMappingProfile>();
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
