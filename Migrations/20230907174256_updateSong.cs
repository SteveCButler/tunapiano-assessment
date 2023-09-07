using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tunapiano.Migrations
{
    public partial class updateSong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Songs_SongId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_SongId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "Artists");

            migrationBuilder.RenameColumn(
                name: "Artist_id",
                table: "Songs",
                newName: "ArtistId");

            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Genres",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ArtistId",
                table: "Genres",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Artists_ArtistId",
                table: "Genres",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Artists_ArtistId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Artists_ArtistId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Genres_ArtistId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Genres");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Songs",
                newName: "Artist_id");

            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "Artists",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_SongId",
                table: "Artists",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Songs_SongId",
                table: "Artists",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id");
        }
    }
}
