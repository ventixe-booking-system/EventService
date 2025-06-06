
using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class Event
{
    public string Id { get; set; } = null!;
    public string? Image { get; set; }
    public string? Title { get; set; } = null!;

    public DateTime EventDate { get; set; }
    public string? Location { get; set; } = null!;
    public string? Description { get; set; }
    public List<Package>? Packages { get; set; }
}

public class Package {
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? SeatingType { get; set; }
    public string? Placement { get; set; }
    public decimal? Price { get; set; }
    public string? Currency { get; set; }
}


