using Application.Dtos.Files;
using Application.Dtos.Folders;
using Application.Files.Commands.Create;
using Application.Files.Commands.Delete;
using Application.Files.Commands.Update;
using Application.Files.Queries.GetAll;
using Application.Files.Queries.GetById;
using Application.Files.Queries.GetFilteredSorted;
using Application.Services.Interfaces;
using MediatR;

namespace Web.Endpoints;

public static class StoredFileEndpoints
{
    public static void MapStoredFileEndpoints(this WebApplication app)
    {
        var fileGroup = app.MapGroup("/files").WithTags("Files").WithTags("Files");

        fileGroup.MapGet("/", async (ISender sender, GetAllQuery query) =>
        {
            var files = await sender.Send(query);

            return Results.Ok(files);
        }).RequireAuthorization("Read");
        
        fileGroup.MapGet("/filter", async (ISender sender, [AsParameters] GetFilteredSortedQuery query) =>
        {
            var files = await sender.Send(query);
            
            return Results.Ok(files);
        }).RequireAuthorization("Read");
        
        fileGroup.MapGet("/{id:guid}", async (ISender sender, GetByIdQuery query) =>
        {
            var file = await sender.Send(query);
            if (file == null) return Results.NotFound();
            
            return Results.Ok(file);
        }).RequireAuthorization("Read");

        fileGroup.MapPost("/", async (ISender sender, CreateQuery query) =>
        {
            var newFile = await sender.Send(query);
            
            return Results.Created($"/files/{newFile.Id}", newFile.Id);
        }).RequireAuthorization("ReadWrite");

        fileGroup.MapPut("/{id:guid}", async (ISender sender, UpdateQuery query) =>
        {
            var updated = await sender.Send(query);
            if (updated) return Results.Ok(updated);
            
            return Results.BadRequest();
        }).RequireAuthorization("ReadWrite");

        fileGroup.MapDelete("/{id:guid}", async (ISender sender, DeleteQuery query) =>
        {
            var deleted = await sender.Send(query);
            if (deleted) return Results.Ok(deleted);
            
            return Results.NotFound();
        }).RequireAuthorization("AdminPolicy");
    }
}