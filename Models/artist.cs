using System.ComponentModel.DataAnnotations;

namespace tunapiano.Models;

public class Artist
{

    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Bio { get; set; } = string.Empty;
}