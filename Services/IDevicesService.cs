namespace CrudOperation.Services;

public interface IDevicesService
{
    IEnumerable<SelectListItem> GetDevices();
}
