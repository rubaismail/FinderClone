using FinderClone.Dtos;
using FinderClone.Services.Interfaces;

namespace FinderClone.Endpoints;

public static class FolderEndpoints
{
    public static void MapFolderEndpoints(this WebApplication app)
    {
        var folderGroup = app.MapGroup("/folders").WithTags("Folders");
        
        folderGroup.MapGet("/", async (IFolderService folderService) =>
        {
            var folders = await folderService.GetAllFoldersAsync();
           
            return Results.Ok(folders);
        }).RequireAuthorization("Read");

        folderGroup.MapGet("/{name}", async (string name, IFolderService folderService) =>
        {
            var folders = await folderService.GetFoldersByNameAsync(name);
            if (folders.Count == 0)
                return Results.NotFound();
            
            return Results.Ok(folders);
        }).RequireAuthorization("Read");

        folderGroup.MapGet("/{id:guid}", async (Guid id, IFolderService folderService) =>
        {
            var folder = await folderService.GetFolderByIdAsync(id);
            
            if (folder == null) return Results.NotFound();
            return Results.Ok(folder);
        }).RequireAuthorization("Read");

        folderGroup.MapPost("/", async (CreateFolderDto folderDto, IFolderService folderService) =>
        {
            var newFolder = await folderService.AddFolderAsync(folderDto);

            return Results.Created($"/folders/{newFolder.Id}", newFolder.Id);
        }).RequireAuthorization("ReadWrite");

        folderGroup.MapPut("/{id:guid}", async (Guid id, UpdateFolderDto folderDto, IFolderService folderService) =>
        {
            
            var updated = await folderService.UpdateFolderAsync(folderDto, id);
            
            if (updated) return Results.Ok();
            return Results.BadRequest();
        }).RequireAuthorization("ReadWrite");

        folderGroup.MapDelete("/{id:guid}", async (Guid id, IFolderService folderService) =>
        {
            var deleted = await folderService.DeleteFolderAsync(id);
            
            if(deleted) return Results.Ok();
            return Results.NotFound();
        }).RequireAuthorization("AdminPolicy");
    }
}