namespace CrudOperation.Services;

public interface ICategoriesService
{
    IEnumerable<SelectListItem> GetCategories();
}
