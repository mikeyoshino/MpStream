using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Models
{
    public class TvShowEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(120)]
        [MinLength(3)]
        public string Title { get; set; }

        [MinLength(3)]
        public string Description { get; set; }
        [Required]
        public string Sound { get; set; }

        public string Trailer { get; set; }
        [Required]
        public int NumberOfSeason { get; set; }
        [Required]
        public string Status { get; set; }
        public ICollection<TvShowWithGenre> TvShowWithGenres { get; set; }
        public ICollection<Season> Seasons { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }

    public class TvShowGenre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(120)]
        [MinLength(3)]
        public string Name { get; set; }
        [MinLength(3)]
        public string Description { get; set; }
        public ICollection<TvShowWithGenre> TvShowWithGenres { get; set; }
    }

    public class TvShowWithGenre
    {
        public int Id { get; set; }
        public TvShowEntity TvShowEntity { get; set; }
        [Required]
        public int TvShowEntityId { get; set; }
        public TvShowGenre TvShowGenre { get; set; }
        [Required]
        public int TvShowGenreId { get; set; }
    }

    public class Season
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int NumberOfEpisode { get; set; }
        public TvShowEntity TvShowEntity { get; set; }
        [Required]
        public int TvShowEntityId { get; set; }
        public ICollection<Episode> Episodes { get; set; }
    }
    public class Episode
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Season Season { get; set; }
        [Required]
        public int SeasonId { get; set; }
        [Required]
        public string EmbedLink { get; set; }
        [Required]
        public string Language { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public TvShowEntity TvShowEntity { get; set; }
    }
}
