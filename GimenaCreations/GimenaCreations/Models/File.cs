namespace GimenaCreations.Models;

public class File
{
    public int Id { get; set; }
    public string Path { get; set; }
    public int OrderItemId { get; set; }
    public OrderItem OrderItem { get; set; }
}
