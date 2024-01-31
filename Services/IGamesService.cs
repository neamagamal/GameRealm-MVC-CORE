namespace CrudOperation.Services;

public interface IGamesService
{
    IEnumerable<Game> GetAll();
    Game? GetById(int id);
    Task CreateGame(CreateGameFormVM game);
    Task<Game?> Update(EditeGameFormVm model);
    bool Delete(int id);
}
