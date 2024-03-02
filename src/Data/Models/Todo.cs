namespace Data.Models;

public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Added { get; set; }
    public DateTime? Updated { get; set; }
}