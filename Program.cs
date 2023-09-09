using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using tunapiano.Models;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//ADD CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7040")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<TunapianoDbContext>(builder.Configuration["TunapianoDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();
//Add for Cors
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//   ### ARTIST ENDPOINTS  ###  

//Get All Artists - #11
app.MapGet("/api/artists", (TunapianoDbContext db) =>
{
    return db.Artists.ToList();

});

//Create New Artist - #1
app.MapPost("/api/artists", (TunapianoDbContext db, Artist artist) =>
{
    db.Artists.Add(artist);
    db.SaveChanges();
    return Results.Created($"/api/artist/artist.Id", artist);
});



//Update existing Artist - #4
app.MapPut("/api/artists/{artistId}", (TunapianoDbContext db, Artist artist, int id) =>
{
    Artist artistToUpdate = db.Artists.SingleOrDefault(a => a.Id == id);
    if (artistToUpdate == null)
    {
        return Results.NotFound();
    }
    artistToUpdate.Name = artist.Name;
    artistToUpdate.Age = artist.Age;
    artistToUpdate.Bio = artist.Bio;

    db.SaveChanges();
    return Results.Created($"/api/artists/artist.id", artist);

});


//Delete existing Artist - #17
app.MapDelete("/api/artists/{artistId}", (TunapianoDbContext db, int id) =>
{
    var artist = db.Artists.SingleOrDefault(a => a.Id == id);
    if (artist == null)
    {
        return Results.NotFound();
    }
    db.Artists.Remove(artist);
    db.SaveChanges();
    return Results.NoContent();

});


// Search Songs by Artist - #14
app.MapGet("/api/songs/artist={artistId}", (TunapianoDbContext db, int id) =>
{
    var songs = db.Artists.Where(s => s.Id == id).Include(x => x.Songs).ToList();
    if (songs == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(songs);

});

// Search songs by Artist Name
app.MapGet("/api/songs/artistName={name}", (TunapianoDbContext db, string name) =>
{
    var songs = db.Artists.Where(s => s.Name.Contains(name)).Include(x => x.Songs).ToList();
    if (songs == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(songs);

});


//   ### SONG ENDPOINTS  ###  

//Get All Songs - #9
app.MapGet("/api/songs", (TunapianoDbContext db) =>
{
    return db.Songs.ToList();
});

//Get Song by ID - inlcude Artist and Genre - #
app.MapGet("/api/songs/{songId}", (TunapianoDbContext db, int id) =>
{

    var song = db.Songs.Include(x => x.Artist).Include(x => x.Genres).FirstOrDefault(x => x.Id == id);
    if (song == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(song);
   
});

//Create a new Song - #18
app.MapPost("/api/songs", (TunapianoDbContext db, Song song) =>
{
    db.Songs.Add(song);
    db.SaveChanges();
    return Results.Created($"/api/songs/song.id", song);

});

//Assign genre to song
app.MapPost("/api/songGenre", (TunapianoDbContext db, int songId, int genreId) =>
{
    var song = db.Songs.SingleOrDefault(s => s.Id == songId);
    var genre = db.Genres.SingleOrDefault(g => g.Id == genreId);

    if (song.Genres == null)
    {
        song.Genres = new List<Genre>();
    }
    song.Genres.Add(genre);
    db.SaveChanges();
    return song;

});

//Update existing Song - #2
app.MapPut("/api/songs/{songId}", (TunapianoDbContext db, Song song, int id) =>
{
    Song songToUpdate = db.Songs.SingleOrDefault(s => s.Id == id);
    if (songToUpdate == null)
    {
        return Results.NotFound();
    }
    songToUpdate.Title = song.Title;
    songToUpdate.ArtistId = song.ArtistId;
    songToUpdate.Album = song.Album;
    songToUpdate.Length = song.Length;

    db.SaveChanges();
    return Results.Created($"/api/songs/song.id", song);

});

//Delete existing Song - #15
app.MapDelete("/api/songs/{songId}", (TunapianoDbContext db, int id) =>
{
    var song = db.Songs.SingleOrDefault(s => s.Id == id);
    if (song == null)
    {
        return Results.NotFound();
    }
    db.Songs.Remove(song);
    db.SaveChanges();
    return Results.NoContent();
});


//   ### GENRE ENDPOINTS  ### 

// Get All Genres - #10
app.MapGet("/api/genres", (TunapianoDbContext db) =>
{
    return db.Genres.ToList();
});

// Create new Genre - #19
app.MapPost("/api/genres", (TunapianoDbContext db, Genre genre) =>
{
    db.Genres.Add(genre);
    db.SaveChanges();
    return Results.Created($"/api/genre/genre.id", genre);
});



// Update existing Genre - #3
app.MapPut("/api/genres/{genreId}", (TunapianoDbContext db, Genre genre, int id) =>
{
    Genre genreToUpdate = db.Genres.SingleOrDefault(gen => gen.Id == id);
    if (genreToUpdate == null)
    {
        return Results.NotFound();
    }
    genreToUpdate.Description = genre.Description;
    db.SaveChanges();
    return Results.Created($"/api/genres/genre.id", genre);
});

//Delete existing Genre - #16
app.MapDelete("/api/genres/{genreId}", (TunapianoDbContext db, int id) =>
{
    var genre = db.Genres.SingleOrDefault(gen => gen.Id==id);
    db.Genres.Remove(genre);
    db.SaveChanges();
    return Results.NoContent();
});



// Search Songs by GenreId - #13
app.MapGet("/api/genres/genre={genreId}", (TunapianoDbContext db, int id) =>
{
    var genreWithSongs = db.Genres.Where(s => s.Id == id).Include(x => x.Songs).ToList();

    if (genreWithSongs == null)
    {
        return Results.NotFound();
    }


    return Results.Ok(genreWithSongs);

});










app.Run();


