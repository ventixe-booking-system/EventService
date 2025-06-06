
namespace Business.Models;

public class CreatePackageRequest
{
    public string Title { get; set; } = null!;
    public string SeatingType { get; set; } = null!;
    public string Placement { get; set; } = null!;
    public decimal Price { get; set; }
    public string Currency { get; set; } = null!;
}
