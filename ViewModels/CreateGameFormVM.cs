
namespace CrudOperation.ViewModels;
public class CreateGameFormVM : GameFormVm
{

    [AllowedExtention(FileSet.AllowedExtensions), MaxSize(FileSet.MaxFileSizeInBytes)]
    public IFormFile Cover { get; set; } = default!;

}
