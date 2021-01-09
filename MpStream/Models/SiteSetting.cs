using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MpStream.Models
{
    public class SiteSetting
    {
        public int Id { get; set; }
        public string SiteTitle { get; set; }
        public string SiteDescription { get; set; }
        public string TmdbApiKey { get; set; }
        public string TmdbApiKeyLanguage { get; set; }
        public bool AutoCompleteGenre { get; set; }
    }

    [NotMapped]
    public class TmdbMovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Runtime { get; set; }
        public string Status { get; set; }
        public string Overview { get; set; }
        public string Original_title { get; set; }
        public string Original_language { get; set; }
        public decimal Vote_average { get; set; }
        public int Vote_count { get; set; }
        public int Revenue { get; set; }
        public DateTime Release_date { get; set; }
        public Genres[] Genres { get; set; }
        public string Poster_path { get; set; }
    }
    [NotMapped]
    public class Genres
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    [NotMapped]
    public class Video
    {
        public int Id { get; set; }
        public VideoResult[] Results { get; set; }
    }
    [NotMapped]
    public class VideoResult
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public int Size { get; set; }
    }

    [NotMapped]
    public class TmdbTvShowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Original_name { get; set; }
        public string Overview { get; set; }
        public decimal Popularity { get; set; }
        public string Poster_path { get; set; }
        public string Status { get; set; }
        public decimal Vote_average { get; set; }
        public int Vote_count { get; set; }
        public int Number_of_episodes { get; set; }
        public int Number_of_seasons { get; set; }
        public string Backdrop_path { get; set; }
        public DateTime First_air_date { get; set; }
        public TvGenres[] Genres { get; set; }
        public bool isRequestSucceed { get; set; } = true;
    }
    [NotMapped]
    public class TvGenres
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
