using Application.Folders.Commands.CreateFolder;
using Application.Folders.Commands.Delete;
using Application.Folders.Commands.Update;
using Application.Folders.Queries.GetAll;
using Application.Folders.Queries.GetById;
using Application.Folders.Queries.GetFilteredSorted;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Endpoints;

public static class FolderEndpoints
{
        public static void MapFolderEndpoints(this WebApplication app)
    {
        var folderGroup = app.MapGroup("/folders").WithTags("Folders");

        folderGroup.MapGet("/", async ([FromServices] ISender sender) =>
        {
            var folders = await sender.Send(new GetAllFolderQuery());

            return Results.Ok(folders);
        }).RequireAuthorization("Read");

        folderGroup.MapPost("/filter",
            async ([FromServices] ISender sender, [FromBody] GetFilteredSortedFolderQuery folderQuery) =>
            {
                var folders = await sender.Send(folderQuery);

                return Results.Ok(folders);
            }).RequireAuthorization("Read");

        folderGroup.MapGet("/{id:guid}",
            async ([FromServices] ISender sender, [AsParameters] Guid id) => //GetByIdFolderQuery folderQuery) =>
            {
                var folder = await sender.Send(new GetByIdFolderQuery()
                {
                    Id = id
                });

                if (folder == null) return Results.NotFound();
                return Results.Ok(folder);
            }).RequireAuthorization("Read");

        folderGroup.MapPost("/", async ([FromServices] ISender sender, [FromBody] CreateFolderCommand folderCommand) =>
        {
            var newFolder = await sender.Send(folderCommand);

            return Results.Created($"/folders/{newFolder.Id}", newFolder.Id);
        }).RequireAuthorization("ReadWrite");

        folderGroup.MapPut("/{id:guid}",
            async ([FromServices] ISender sender, [AsParameters] Guid id, [FromBody] UpdateFolderCommand folderCommand) =>
            {
                folderCommand.Id = id;
                var updated = await sender.Send(folderCommand);

                if (updated) return Results.Ok();
                return Results.BadRequest();
            }).RequireAuthorization("ReadWrite");

        folderGroup.MapDelete("/{id:guid}", async ([FromServices] ISender sender, [AsParameters] Guid id) =>
        {
            var deleted = await sender.Send(new DeleteFolderCommand
            {
                Id = id
            });

            if (deleted) return Results.Ok();
            return Results.NotFound();
        }).RequireAuthorization("AdminPolicy");
    }
}


/*
 public  class FolderEndpoints : EndpointGroupBase
 
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .WithTags("Folders")
            .RequireAuthorization()
            .MapGet(GetAllFolders)
            .MapPost(FilterFolders, "filter")
            .MapGet(GetFolderById, "{id:guid}")
            .MapPost(CreateFolder)
            .MapPut(UpdateFolder, "{id:guid}")
            .MapDelete(DeleteFolder, "{id:guid}");
    }

    public async Task<Ok<List<GetManyFoldersDto>>> GetAllFolders(ISender sender)
    {
        var folders = await sender.Send(new GetAllFolderQuery());

        return TypedResults.Ok(folders);
    }

    public async Task<Ok<List<GetManyFoldersDto>>> FilterFolders(ISender sender, [FromBody] GetFilteredSortedFolderQuery query)
    {
        var folders = await sender.Send(query);
        return TypedResults.Ok(folders);
    }
    public async Task<Results<Ok<GetFolderDto>, NotFound>> GetFolderById(ISender sender, Guid id)
    {
        var folder = await sender.Send(new GetByIdFolderQuery { Id = id });
        return folder is null ? TypedResults.NotFound() : TypedResults.Ok(folder);
    }

    public async Task<Created<Guid>> CreateFolder(ISender sender, CreateFolderQuery query)
    {
        var createdFolder = await sender.Send(query);
        return TypedResults.Created($"/folders/{createdFolder.Id}", createdFolder.Id);
    }

    public async Task<Results<Ok, BadRequest>> UpdateFolder(ISender sender, Guid id, UpdateFolderQuery query)
    {
        if (id != query.Id) query.Id = id;

        var result = await sender.Send(query);
        return result ? TypedResults.Ok() : TypedResults.BadRequest();
    }

    public async Task<Results<Ok, NotFound>> DeleteFolder(ISender sender, Guid id)
    {
        var deleted = await sender.Send(new DeleteFolderQuery { Id = id });
        return deleted ? TypedResults.Ok() : TypedResults.NotFound();
    }
}
*/