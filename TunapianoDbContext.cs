using Microsoft.EntityFrameworkCore;

namespace tunapiano.Models;

public class TunapianoDbContext : DbContext
{
    public DbSet<Song> Songs { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Artist> Artists { get; set; }
    //public DbSet<Song_Genre> Song_Genres { get; set; }



    public TunapianoDbContext(DbContextOptions<TunapianoDbContext> context) : base(context)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      

        // seed data with Artists
        modelBuilder.Entity<Artist>().HasData(new Artist[]
        {
        new Artist {Id = 1, Name="Lyle Lovett", Age=65, Bio="an American country singer, songwriter, record producer and actor. "},
        new Artist {Id = 2, Name="Tim O'Brien", Age=69, Bio="an American country and bluegrass musician. In addition to singing, he plays guitar, fiddle, mandolin, banjo, bouzouki and mandocello. "},
        
        });

        // seed data with Songs
        modelBuilder.Entity<Song>().HasData(new Song[]
        {
        new Song {Id = 1, Album="Step Inside This House", ArtistId=1, Title="Step Inside This House", Length=280 },
        new Song {Id = 2, Album="Real Time", ArtistId=2, Title="Walk Beside Me", Length=253},

        });

        // see data with Genre
        modelBuilder.Entity<Genre>().HasData(new Genre[] { 
        
            new Genre {Id = 1, Description="Country"},
            new Genre {Id = 2, Description="Bluegrass"},
            new Genre {Id = 3, Description="Rock"},
            new Genre {Id = 4, Description="Christian"},
            new Genre {Id = 5, Description="Oldtime"}
        });

        //SEED join table songGenre
        //modelBuilder.Entity<Song_Genre>().HasData(new Song_Genre[]
        //{
        //    new Song_Genre {Id=1, Song_Id=1, Genre_Id=1},
        //    new Song_Genre {Id=2, Song_Id=2, Genre_Id=2},
        //});


    }

}
