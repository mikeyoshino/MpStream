using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Models
{
    public class MovieEntity
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(120)]
        public string Title { get; set; }
        public byte[] PosterImage { get; set; }
        public ICollection<MovieWithGenre> MovieWithGenres { get; set; }
        public string Player { get; set; }
        public string Tag { get; set; }
        public int Score { get; set; }
        public string Description { get; set; }
        public string Sound { get; set; }
        public string TrailerId { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        public bool IsFeatured { get; set; } = false;
    }

    public class MoviePlayer
    {
        public int Id { get; set; }
        public MovieEntity MovieEntity { get; set; }
        [Required]
        public int MovieEntityId { get; set; }
        [Required]
        public string EmbedCode { get; set; }
    }

    public class MovieWithGenre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public MovieEntity MovieEntity { get; set; }
        [Required]
        public int MovieEntityId { get; set; }
        public MovieGenreEntity MovieGenreEntity { get; set; }
        [Required]
        public int MovieGenreEntityId { get; set; }
    }

    public class MovieGenreEntity
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(120)]
        public string Name { get; set; }
        public string Tag { get; set; }
        public ICollection<MovieWithGenre> MovieWithGenres { get; set; }
    }
}
