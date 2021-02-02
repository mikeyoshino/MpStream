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
        public string TitleTH { get; set; }
        public string PosterImage { get; set; }
        public ICollection<MovieWithGenre> MovieWithGenres { get; set; }
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public string PlayerThree { get; set; }
        public string PlayerFour { get; set; }
        public string Tag { get; set; }
        public decimal Score { get; set; }
        public int Runtime { get; set; }
        public int Revenue { get; set; }
        public int Vote_count { get; set; }
        public int ReleaseYear { get; set; }
        public string Description { get; set; }
        public string Sound { get; set; }
        public string TrailerId { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        public bool IsFeatured { get; set; } = false;
        public MovieLike MovieLike { get; set; }
        public ICollection<MovieComment> MovieComments { get; set; }
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

    public class MovieLike
    {
        public int Id { get; set; }
        public MovieEntity MovieEntity { get; set; }
        public int MovieEntityId { get; set; }
        public int LikeCount { get; set; }
    }

    public class MovieComment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public MovieEntity MovieEntity { get; set; }
        public int MovieEntityId { get; set; }
    }
}
