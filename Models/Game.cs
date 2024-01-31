namespace CrudOperation.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Cover { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>();
}

