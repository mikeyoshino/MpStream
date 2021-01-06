using Microsoft.EntityFrameworkCore;
using MpStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        public DbSet<MovieEntity> MovieEntity { get; set; }
        public DbSet<MovieGenreEntity> MovieGenreEntities { get; set; }
        public DbSet<MoviePlayer> MoviePlayers { get; set; }
        public DbSet<MovieWithGenre> MovieWithGenres { get; set; }
        public DbSet<TvShowEntity> TvShowEntities { get; set; }
        public DbSet<TvShowGenre>  TvShowGenres { get; set; }
        public DbSet<TvShowWithGenre> TvShowWithGenres { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TvShowRegion> TvShowRegions { get; set; }
    }
}
