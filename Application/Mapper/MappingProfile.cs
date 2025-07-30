using Application.Dtos.Files;
using Application.Dtos.Folders;
using Application.Files.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Folder,GetManyFoldersDto>();
        CreateMap<Folder, GetFolderDto>();
        //CreateMap<CreateFolderDto, Folder>();
        CreateMap<CreateFileCommand, Folder>();

        
        CreateMap<StoredFile, GetFileDto>();
        CreateMap<CreateFileDto, StoredFile>();
    }
}