using tunapiano.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

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



//Create New Artist - #1
app.MapPost("/api/artists", (TunapianoDbContext db, Artist artist) =>
{
    db.Artists.Add(artist);
    db.SaveChanges();
    return Results.Created($"/api/artist/artist.Id", artist);
});

//Get All Artists - #11
app.MapGet("/api/artists", (TunapianoDbContext db) =>
{
    return db.Artists.ToList();

});

//Update existing Artist - #4
app.MapPut("/api/artists/{artistId}", (TunapianoDbContext db, Artist artist, int id) =>
{
    Artist artistToUpdate = db.Artists.SingleOrDefault(a => a.Id == id);
    if(artistToUpdate == null)
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



app.Run();


