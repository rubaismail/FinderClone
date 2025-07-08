using FinderClone.Dtos;
using FinderClone.Services.Interfaces;

namespace FinderClone.Endpoints;

public static class StoredFileEndpoints
{
    public static void MapStoredFileEndpoints(this WebApplication app)
    {
        var fileGroup = app.MapGroup("/files").WithTags("Files").WithTags("Files");

        fileGroup.MapGet("/", async (IStoredFileService storedFileService) =>
        {
            var files = await storedFileService.GetAllFilesAsync();
            
            return Results.Ok(files);
        }).RequireAuthorization("Read");

        fileGroup.MapGet("/{name}", async (String name, IStoredFileService storedFileService) =>
        {
            var files = await storedFileService.GetFileByNameAsync(name);
            if (files.Count == 0) return Results.NotFound();
            
            return Results.Ok(files);
        }).RequireAuthorization("Read");

        fileGroup.MapGet("/{id:guid}", async (Guid id, IStoredFileService storedFileService) =>
        {
            var file = await storedFileService.GetFileByIdAsync(id);
            if (file == null) return Results.NotFound();
            
            return Results.Ok(file);
        }).RequireAuthorization("Read");

        fileGroup.MapPost("/", async (CreateFileDto createFileDto, IStoredFileService storedFileService) =>
        {
            var newFile = await storedFileService.AddFileAsync(createFileDto);
            
            return Results.Created($"/files/{newFile.Id}", newFile.Id);
        }).RequireAuthorization("ReadWrite");

        fileGroup.MapPut("/{id:guid}", async (Guid id, UpdateFileDto fileDto, IStoredFileService storedFileService) =>
        {
            var updated = await storedFileService.UpdateFileAsync(fileDto, id);
            if (updated) return Results.Ok(updated);
            
            return Results.BadRequest();
        }).RequireAuthorization("ReadWrite");

        fileGroup.MapDelete("/{id:guid}", async (Guid id, IStoredFileService storedFileService) =>
        {
            var deleted = await storedFileService.DeleteFileAsync(id);
            if (deleted) return Results.Ok(deleted);
            
            return Results.NotFound();
        }).RequireAuthorization("AdminPolicy");
    }
}