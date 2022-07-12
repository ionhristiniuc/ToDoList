using AutoMapper;
using ToDoList.API.Configuration;

namespace ToDoList.API.Tests.Utils;

public static class MapperBuilder
{
    public static IMapper Create()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new AppMappingProfile());
        });
        return mappingConfig.CreateMapper();
    }
}