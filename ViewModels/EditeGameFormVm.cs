namespace CrudOperation.ViewModels;

public class EditeGameFormVm : GameFormVm
{
    public string? CurrantCover { get; set; }
    public int Id { get; set; }
    [AllowedExtention(FileSet.AllowedExtensions), MaxSize(FileSet.MaxFileSizeInBytes)]
    public IFormFile? Cover { get; set; } = default!;
}
