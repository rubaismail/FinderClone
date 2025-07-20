namespace Application.Dtos.Folders;

public class GetFolderDto
{
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Guid>? SubFoldersIds { get; set; }
        public List<Guid>? FilesIds { get; set; }
        public Guid? ParentFolderId { get; set; }
        public string? RelativePath { get; set; }
}

