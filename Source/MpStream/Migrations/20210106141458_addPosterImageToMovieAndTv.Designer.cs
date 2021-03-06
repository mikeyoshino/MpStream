﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MpStream.Data;

namespace MpStream.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210106141458_addPosterImageToMovieAndTv")]
    partial class addPosterImageToMovieAndTv
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("MpStream.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("EmbedLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<string>("SeasonNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("MpStream.Models.MovieEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<string>("Player")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PosterImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Sound")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("TrailerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovieEntity");
                });

            modelBuilder.Entity("MpStream.Models.MovieGenreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MovieGenreEntities");
                });

            modelBuilder.Entity("MpStream.Models.MoviePlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("EmbedCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieEntityId");

                    b.ToTable("MoviePlayers");
                });

            modelBuilder.Entity("MpStream.Models.MovieWithGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieEntityId")
                        .HasColumnType("int");

                    b.Property<int>("MovieGenreEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieEntityId");

                    b.HasIndex("MovieGenreEntityId");

                    b.ToTable("MovieWithGenres");
                });

            modelBuilder.Entity("MpStream.Models.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfEpisode")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TvShowEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TvShowEntityId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("MpStream.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TvShowEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TvShowEntityId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("MpStream.Models.TvShowEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfSeason")
                        .HasColumnType("int");

                    b.Property<byte[]>("PosterImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Sound")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Trailer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TvShowEntities");
                });

            modelBuilder.Entity("MpStream.Models.TvShowGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("TvShowGenres");
                });

            modelBuilder.Entity("MpStream.Models.TvShowRegion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TvShowEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TvShowEntityId");

                    b.ToTable("TvShowRegions");
                });

            modelBuilder.Entity("MpStream.Models.TvShowWithGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("TvShowEntityId")
                        .HasColumnType("int");

                    b.Property<int>("TvShowGenreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TvShowEntityId");

                    b.HasIndex("TvShowGenreId");

                    b.ToTable("TvShowWithGenres");
                });

            modelBuilder.Entity("MpStream.Models.Episode", b =>
                {
                    b.HasOne("MpStream.Models.Season", "Season")
                        .WithMany("Episodes")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Season");
                });

            modelBuilder.Entity("MpStream.Models.MoviePlayer", b =>
                {
                    b.HasOne("MpStream.Models.MovieEntity", "MovieEntity")
                        .WithMany()
                        .HasForeignKey("MovieEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieEntity");
                });

            modelBuilder.Entity("MpStream.Models.MovieWithGenre", b =>
                {
                    b.HasOne("MpStream.Models.MovieEntity", "MovieEntity")
                        .WithMany("MovieWithGenres")
                        .HasForeignKey("MovieEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MpStream.Models.MovieGenreEntity", "MovieGenreEntity")
                        .WithMany("MovieWithGenres")
                        .HasForeignKey("MovieGenreEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MovieEntity");

                    b.Navigation("MovieGenreEntity");
                });

            modelBuilder.Entity("MpStream.Models.Season", b =>
                {
                    b.HasOne("MpStream.Models.TvShowEntity", "TvShowEntity")
                        .WithMany("Seasons")
                        .HasForeignKey("TvShowEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TvShowEntity");
                });

            modelBuilder.Entity("MpStream.Models.Tag", b =>
                {
                    b.HasOne("MpStream.Models.TvShowEntity", "TvShowEntity")
                        .WithMany("Tags")
                        .HasForeignKey("TvShowEntityId");

                    b.Navigation("TvShowEntity");
                });

            modelBuilder.Entity("MpStream.Models.TvShowRegion", b =>
                {
                    b.HasOne("MpStream.Models.TvShowEntity", "TvShowEntity")
                        .WithMany("TvShowRegions")
                        .HasForeignKey("TvShowEntityId");

                    b.Navigation("TvShowEntity");
                });

            modelBuilder.Entity("MpStream.Models.TvShowWithGenre", b =>
                {
                    b.HasOne("MpStream.Models.TvShowEntity", "TvShowEntity")
                        .WithMany("TvShowWithGenres")
                        .HasForeignKey("TvShowEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MpStream.Models.TvShowGenre", "TvShowGenre")
                        .WithMany("TvShowWithGenres")
                        .HasForeignKey("TvShowGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TvShowEntity");

                    b.Navigation("TvShowGenre");
                });

            modelBuilder.Entity("MpStream.Models.MovieEntity", b =>
                {
                    b.Navigation("MovieWithGenres");
                });

            modelBuilder.Entity("MpStream.Models.MovieGenreEntity", b =>
                {
                    b.Navigation("MovieWithGenres");
                });

            modelBuilder.Entity("MpStream.Models.Season", b =>
                {
                    b.Navigation("Episodes");
                });

            modelBuilder.Entity("MpStream.Models.TvShowEntity", b =>
                {
                    b.Navigation("Seasons");

                    b.Navigation("Tags");

                    b.Navigation("TvShowRegions");

                    b.Navigation("TvShowWithGenres");
                });

            modelBuilder.Entity("MpStream.Models.TvShowGenre", b =>
                {
                    b.Navigation("TvShowWithGenres");
                });
#pragma warning restore 612, 618
        }
    }
}
