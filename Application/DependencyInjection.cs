using Application.Folders.Queries.GetFilteredSorted;
using Application.Mapper;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IFolderService, FolderService>();
        builder.Services.AddScoped<IStoredFileService, StoredFileService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
      
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetFilteredSortedFolderQueryHandler).Assembly);
        });
        
        builder.Services.AddAutoMapper(cfg => 
        {
            cfg.AddProfile<MappingProfile>();
        }, typeof(MappingProfile).Assembly);
    }
}