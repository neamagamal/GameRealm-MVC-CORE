namespace CrudOperation.ViewModels;

public class GameFormVm
{
    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(5000)]
    public string Description { get; set; } = string.Empty;
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
    [Display(Name = "Devices")]
    public List<int> SelectedDevices { get; set; } = default!;
    public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
}
