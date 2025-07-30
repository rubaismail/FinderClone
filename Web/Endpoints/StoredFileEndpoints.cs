using Application.Files.Commands.Create;
using Application.Files.Commands.Delete;
using Application.Files.Commands.Update;
using Application.Files.Queries.GetAll;
using Application.Files.Queries.GetById;
using Application.Files.Queries.GetFilteredSorted;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints;

public static class StoredFileEndpoints
{
    public static void MapStoredFileEndpoints(this WebApplication app)
    {
        var fileGroup = app.MapGroup("/files").WithTags("Files");

        fileGroup.MapGet("/", async ([FromServices] ISender sender) =>
        {
            var files = await sender.Send(new GetAllQuery());

            return Results.Ok(files);
        }).RequireAuthorization("Read");
        
        fileGroup.MapPost("/filter", async ([FromServices] ISender sender, [FromBody] GetFilteredSortedFileQuery fileQuery) =>
        {
            var files = await sender.Send(fileQuery);
            
            return Results.Ok(files);
        }).RequireAuthorization("Read");
        
        fileGroup.MapGet("/{id:guid}", async ([FromServices] ISender sender, [AsParameters] Guid id) =>
        {
            var file = await sender.Send(new GetByIdFileQuery
            {
                Id = id
            });
            if (file == null) return Results.NotFound();
            
            return Results.Ok(file);
        }).RequireAuthorization("Read");

        fileGroup.MapPost("/", async ([FromServices] ISender sender, [FromBody] CreateFileCommand fileCommand) =>
        {
            var newFile = await sender.Send(fileCommand);
            
            return Results.Created($"/files/{newFile.Id}", newFile.Id);
        }).RequireAuthorization("ReadWrite");

        fileGroup.MapPut("/{id:guid}", async ([FromServices] ISender sender, [AsParameters] Guid id, [FromBody] UpdateFileCommand fileCommand) =>
        {
            fileCommand.Id = id;
            var updated = await sender.Send(fileCommand);
            if (updated) return Results.Ok(updated);
            
            return Results.BadRequest();
        }).RequireAuthorization("ReadWrite");

        fileGroup.MapDelete("/{id:guid}", async ([FromServices] ISender sender, [AsParameters] Guid id) =>
        {
            var deleted = await sender.Send(new DeleteFileCommand { Id = id });

            if (deleted)
                return Results.Ok(deleted);

            return Results.NotFound();
        }).RequireAuthorization("AdminPolicy");
    }
}