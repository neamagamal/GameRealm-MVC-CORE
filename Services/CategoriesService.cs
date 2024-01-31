
namespace CrudOperation.Services;

public class CategoriesService : ICategoriesService
{
    private readonly AppDbContext _Context;

    public CategoriesService(AppDbContext context)
    {
        _Context = context;
    }

    public IEnumerable<SelectListItem> GetCategories()
    {
        return _Context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).AsNoTracking()
                .OrderBy(c => c.Text).ToList();
    }
}
