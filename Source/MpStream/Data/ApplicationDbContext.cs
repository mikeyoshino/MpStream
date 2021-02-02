using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MpStream.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext()
        { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<MovieEntity> MovieEntity { get; set; }
        public DbSet<MovieGenreEntity> MovieGenreEntities { get; set; }
        public DbSet<MovieWithGenre> MovieWithGenres { get; set; }
        public DbSet<TvShowEntity> TvShowEntities { get; set; }
        public DbSet<TvShowGenre>  TvShowGenres { get; set; }
        public DbSet<TvShowWithGenre> TvShowWithGenres { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TvShowRegion> TvShowRegions { get; set; }
        public DbSet<MovieLike> MovieLikes { get; set; }
        public DbSet<MovieComment> MovieComments { get; set; }
    }
}
