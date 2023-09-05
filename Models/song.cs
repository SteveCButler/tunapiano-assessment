using System.ComponentModel.DataAnnotations;
namespace tunapiano.Models;

public class Song
{
    public Song()
    {
        this.Genres = new HashSet<Genre>();
    }
    public int Id { get; set; }
    [Required]

    public string Title { get; set; } = string.Empty;
    public int Artist_id { get; set; }
    public string Album { get; set; } = string.Empty;
    public int Length { get; set; }
    public ICollection<Genre> Genres { get; set; }
    public List<Artist> Artists { get; set;}

}
