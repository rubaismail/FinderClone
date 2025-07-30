using MediatR;

namespace Application.Files.Commands.Update;

public class UpdateFileCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    //public UpdateFileDto FileDto { get; set; }
    
    // contents of updatefiledto:
    public string Name { get; set; }
    public Guid ParentFolderId { get; set; }
    public string? RelativePath { get; set; }
    public long? SizeBytes { get; set; }
}