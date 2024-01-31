namespace CrudOperation.Services;

public class GamesService : IGamesService
{
    private readonly AppDbContext _Context;
    private readonly IWebHostEnvironment _WebHostEnvironment;
    private readonly string _ImagesPath;


    public GamesService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _Context = context;
        _WebHostEnvironment = webHostEnvironment;
        _ImagesPath = $"{_WebHostEnvironment.WebRootPath}{FileSet.ImagesPath}";
    }
    public IEnumerable<Game> GetAll()
    {
        return _Context.Games.Include(x => x.Category).Include(d => d.Devices).ThenInclude(dev => dev.device)
            .AsNoTracking()
            .ToList();
    }
    public Game? GetById(int id)
    {
        return _Context.Games.Include(x => x.Category).Include(d => d.Devices).ThenInclude(dev => dev.device)
           .AsNoTracking()
           .SingleOrDefault(x => x.Id == id);
    }
    public async Task CreateGame(CreateGameFormVM game)
    {
        var CoverName = await SaveImage(game.Cover);
        Game CreatedGame = new()
        {
            Name = game.Name,
            Description = game.Description,
            CategoryId = game.CategoryId,
            Cover = CoverName,
            Devices = game.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()

        };
        _Context.Add(CreatedGame);
        _Context.SaveChanges();

    }
    public async Task<Game?> Update(EditeGameFormVm model)
    {
        var game = _Context.Games
            .Include(g => g.Devices)
            .SingleOrDefault(g => g.Id == model.Id);

        if (game is null)
            return null;

        var NewCover = model.Cover is not null;
        var oldCover = game.Cover;

        game.Name = model.Name;
        game.Description = model.Description;
        game.CategoryId = model.CategoryId;
        game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

        if (NewCover)
        {
            game.Cover = await SaveImage(model.Cover!);
        }

        var effectedRows = _Context.SaveChanges();

        if (effectedRows > 0)
        {
            if (NewCover)
            {
                var cover = Path.Combine(_ImagesPath, oldCover);
                File.Delete(cover);
            }

            return game;
        }
        else
        {
            var cover = Path.Combine(_ImagesPath, game.Cover);
            File.Delete(cover);

            return null;
        }
    }

    public bool Delete(int id)
    {
        var game = _Context.Games.Include(g => g.Devices)
            .SingleOrDefault(g => g.Id == id);
        if (game is null)
            return false;
        _Context.Remove(game);
        var effectedRows = _Context.SaveChanges();
        if (effectedRows > 0)
        {
            File.Delete(Path.Combine(_ImagesPath, game.Cover));
        }
        return true;
    }
    private async Task<string> SaveImage(IFormFile cover)
    {
        var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
        var CoverPath = Path.Combine(_ImagesPath, CoverName);
        using var CreatedCover = File.Create(CoverPath);
        await cover.CopyToAsync(CreatedCover);
        return CoverName;
    }


}



