using Application.Dtos.Folders;
using Application.Folders.Commands.CreateFolder;
using Application.Folders.Commands.Delete;
using Application.Folders.Commands.Update;
using Application.Folders.Queries.GetAll;
using Application.Folders.Queries.GetById;
using Application.Folders.Queries.GetFilteredSorted;
using Application.Services.Interfaces;
using MediatR;

namespace Web.Endpoints;

public static class FolderEndpoints
{
    public static void MapFolderEndpoints(this WebApplication app)
    {
        var folderGroup = app.MapGroup("/folders").WithTags("Folders");
        
        folderGroup.MapGet("/", async (ISender sender, GetAllQuery query) =>
        {
            var folders = await sender.Send(query);
           
            return Results.Ok(folders);
        }).RequireAuthorization("Read");
        
        folderGroup.MapGet("/filter", async (ISender sender, [AsParameters] GetFilteredSortedQuery query) =>
        {
            var folders = await sender.Send(query);
            
            return Results.Ok(folders);
        }).RequireAuthorization("Read");

        folderGroup.MapGet("/{id:guid}", async (ISender sender, GetByIdQuery query) =>
        {
            var folder = await sender.Send(query);
            
            if (folder == null) return Results.NotFound();
            return Results.Ok(folder);
        }).RequireAuthorization("Read");
        
        folderGroup.MapPost("/", async (ISender sender, CreateQuery query) =>
        {
            var newFolder = await sender.Send(query);

            return Results.Created($"/folders/{newFolder.Id}", newFolder.Id);
        }).RequireAuthorization("ReadWrite");

        folderGroup.MapPut("/{id:guid}", async (ISender sender, UpdateQuery query) =>
        {
            
            var updated = await sender.Send(query);
            
            if (updated) return Results.Ok();
            return Results.BadRequest();
        }).RequireAuthorization("ReadWrite");

        folderGroup.MapDelete("/{id:guid}", async (ISender sender, DeleteQuery query) =>
        {
            var deleted = await sender.Send(query);
            
            if(deleted) return Results.Ok();
            return Results.NotFound();
        }).RequireAuthorization("AdminPolicy");
    }
}