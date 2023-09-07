﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using tunapiano.Models;

#nullable disable

namespace tunapiano.Migrations
{
    [DbContext(typeof(TunapianoDbContext))]
    partial class TunapianoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.Property<int>("GenresId")
                        .HasColumnType("integer");

                    b.Property<int>("SongsId")
                        .HasColumnType("integer");

                    b.HasKey("GenresId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("GenreSong");
                });

            modelBuilder.Entity("tunapiano.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 65,
                            Bio = "an American country singer, songwriter, record producer and actor. ",
                            Name = "Lyle Lovett"
                        },
                        new
                        {
                            Id = 2,
                            Age = 69,
                            Bio = "an American country and bluegrass musician. In addition to singing, he plays guitar, fiddle, mandolin, banjo, bouzouki and mandocello. ",
                            Name = "Tim O'Brien"
                        });
                });

            modelBuilder.Entity("tunapiano.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ArtistId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Country"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Bluegrass"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Rock"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Christian"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Oldtime"
                        });
                });

            modelBuilder.Entity("tunapiano.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Album")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ArtistId")
                        .HasColumnType("integer");

                    b.Property<int>("Length")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Songs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Album = "Step Inside This House",
                            ArtistId = 1,
                            Length = 280,
                            Title = "Step Inside This House"
                        },
                        new
                        {
                            Id = 2,
                            Album = "Real Time",
                            ArtistId = 2,
                            Length = 253,
                            Title = "Walk Beside Me"
                        });
                });

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.HasOne("tunapiano.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("tunapiano.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("tunapiano.Models.Genre", b =>
                {
                    b.HasOne("tunapiano.Models.Artist", null)
                        .WithMany("Genres")
                        .HasForeignKey("ArtistId");
                });

            modelBuilder.Entity("tunapiano.Models.Song", b =>
                {
                    b.HasOne("tunapiano.Models.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("tunapiano.Models.Artist", b =>
                {
                    b.Navigation("Genres");

                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
