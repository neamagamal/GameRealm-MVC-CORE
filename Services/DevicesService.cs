namespace CrudOperation.Services;

public class DevicesService : IDevicesService
{
    private AppDbContext _Context;

    public DevicesService(AppDbContext Context)
    {
        _Context = Context;
    }

    IEnumerable<SelectListItem> IDevicesService.GetDevices()
    {
        return _Context.Devices.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).AsNoTracking()
                        .OrderBy(d => d.Text).ToList();
    }
}
