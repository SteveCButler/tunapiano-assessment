using System.ComponentModel.DataAnnotations;
namespace tunapiano.Models;

public class Genre
{
    public Genre()
    {
        this.Songs = new HashSet<Song>();
    }
    public int Id { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;
    public ICollection<Song> Songs { get; set; }

}
