using CrudOperation.Services;

namespace CrudOperation.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesService _CategoriesService;
        public readonly IDevicesService _DevicesService;
        public readonly IGamesService _GamesService;

        public GamesController(ICategoriesService categoriesService, IDevicesService devicesService, IGamesService gamesService)
        {
            _CategoriesService = categoriesService;
            _DevicesService = devicesService;
            _GamesService = gamesService;
        }

        public IActionResult Index()
        {
            var detailedGame = _GamesService.GetAll();
            return View(detailedGame);
        }

        public IActionResult GetById(int id)
        {
            var Details = _GamesService.GetById(id);
            if (Details == null)
            {
                return NotFound();
            }
            return View(Details);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormVM Createvm = new CreateGameFormVM()
            {
                Categories = _CategoriesService.GetCategories(),
                Devices = _DevicesService.GetDevices()
            };
            return View(Createvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormVM createModel)
        {
            if (!ModelState.IsValid)
            {
                createModel.Categories = _CategoriesService.GetCategories();
                createModel.Devices = _DevicesService.GetDevices();
                return View(createModel);
            }
            await _GamesService.CreateGame(createModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var UpdatedGame = _GamesService.GetById(id);

            if (UpdatedGame is null)
                return NotFound();

            EditeGameFormVm EditVM = new()
            {
                Id = id,
                Name = UpdatedGame.Name,
                Description = UpdatedGame.Description,
                CategoryId = UpdatedGame.CategoryId,
                SelectedDevices = UpdatedGame.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _CategoriesService.GetCategories(),
                Devices = _DevicesService.GetDevices(),
                CurrantCover = UpdatedGame.Cover
            };

            return View(EditVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditeGameFormVm UpdatedModel)
        {
            if (!ModelState.IsValid)
            {
                UpdatedModel.Categories = _CategoriesService.GetCategories();
                UpdatedModel.Devices = _DevicesService.GetDevices();
                return View(UpdatedModel);
            }

            try
            {
                var game = await _GamesService.Update(UpdatedModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _GamesService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}









