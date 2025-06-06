using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models;

public class CreateEventRequest
{
    public string? Image { get; set; }
    public string? Title { get; set; } = null!;
    public DateTime EventDate { get; set; }
    public string? Location { get; set; } = null!;
    public string? Description { get; set; }
    public List<CreatePackageRequest> Packages { get; set; } = [];
}
