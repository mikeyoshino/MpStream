using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MpStream.Pages.Admin.Movies
{
    public partial class AddMovie : ComponentBase
    {
        public string ImdbId { get; set; }
        public string TitleTH {private get; set; }
        public string PreviewImage { get; set; }
        public MovieEntity MovieEntity { get; set; } = new MovieEntity();
        [Inject]
        public MovieService MovieService { get; set; }
        [Inject]
        public MovieGenreService MovieGenreService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public List<MovieGenreEntity> MovieGenres { get; private set; } = new List<MovieGenreEntity>();
        public List<string> GenreStringList { get; set; } = new List<string>();
        public List<string> SelectedGenreIds { get; set; } = new List<string>();
        public enum SoundChoices
        {
            พากย์ไทย,
            ซับไทย,
            พากย์ไทยซับไทย,
            อังกฤษ
        }
        public string ImageUrl { get; set; }
        public bool ApiLoadSpiner { get; set; } = false;
        public bool MoveSaveSpinner { get; set; } = false;
        public string showStatusMessageApiRequest { get; set; }
        [Inject]
        public IWebHostEnvironment environment { get; set; } 
        IBrowserFile SelectedImage;

        protected override void OnInitialized()
        {
            MovieGenres = MovieService.MovieGenreList();
        }

        async Task UploadImageOnchange(InputFileChangeEventArgs e)
        {
            string format = "image/jpg";
            var imageFile = e.File;
            var resizeFile = await imageFile.RequestImageFileAsync(format, 660, 420);
            var buffer = new byte[resizeFile.Size];
            await resizeFile.OpenReadStream().ReadAsync(buffer);
            SelectedImage = imageFile;
            ImageUrl = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
        }

        async Task SaveMovie()
        {
            MoveSaveSpinner = true;
            if (ImageUrl != null)
            {
                Stream stream = SelectedImage.OpenReadStream();
                var extension = Path.GetExtension(SelectedImage.Name);
                var fileNameBasedOnDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                var path = $"{environment.WebRootPath}\\posters\\{fileNameBasedOnDate + extension}";
                FileStream fileStream = File.Create(path);
                await stream.CopyToAsync(fileStream);
                fileStream.Close();
                MovieEntity.PosterImage = $"/posters/{fileNameBasedOnDate + extension}";

            } else if (PreviewImage != null)
            {
                var path = $"{environment.WebRootPath}" + "\\" + "Posters";
                var fileNameBasedOnDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                var apiImageUrl = $"https://image.tmdb.org/t/p/original/{PreviewImage}";
                await MovieService.DownloadImageAsync(path, fileNameBasedOnDate, new Uri(apiImageUrl));
                MovieEntity.PosterImage = $"/Posters/{fileNameBasedOnDate + ".jpg"}";
            }
            MovieEntity.PublishedDate = DateTime.Now;
            MovieEntity.IsFeatured = false;
            var resuleSaveMovie = MovieService.AddMovie(MovieEntity);
            var resultSaveGenre = MovieService.SaveMovieWithGenre(MovieEntity, SelectedGenreIds);

            if (resuleSaveMovie.IsCompleted && resultSaveGenre.IsCompleted)
            {
                StateHasChanged();
                NavigationManager.NavigateTo("/admin/movie");
            }
            MoveSaveSpinner = false;
        }

        void CheckboxClicked(string Id, object checkedValue)
        {
            if ((bool)checkedValue)
            {
                if (!SelectedGenreIds.Contains(Id))
                {
                    SelectedGenreIds.Add(Id);
                }
            }
            else
            {
                if (SelectedGenreIds.Contains(Id))
                {
                    SelectedGenreIds.Remove(Id);
                }
            }
        }
        public async Task FetchImdbApi(string Id)
        {
            ApiLoadSpiner = true;
            var movieDataTH = await MovieService.FetchTmdbApi(Id);
            var videoTrailer = await MovieService.FetchTmdbTrailerApi(Id);
            if (movieDataTH.isRequestSucceed)
            {
                MovieEntity.Title = movieDataTH.Original_title;
                MovieEntity.Description = movieDataTH.Overview;
                MovieEntity.Score = movieDataTH.Vote_average;
                MovieEntity.Runtime = movieDataTH.Runtime;
                MovieEntity.Revenue = movieDataTH.Revenue;
                MovieEntity.Vote_count = movieDataTH.Vote_count;
                MovieEntity.ReleaseYear = movieDataTH.Release_date.Year;
                PreviewImage = movieDataTH.Poster_path;
                TitleTH = movieDataTH.Title;
                var movieDataEnglish = await MovieService.FetchTmdbApiEnglish(Id);
                if (videoTrailer.Results.Length != 0)
                {
                    MovieEntity.TrailerId = videoTrailer.Results[0].Key;
                }
                var genreInEnglish = movieDataEnglish.Genres;
                foreach (var eachGenre in MovieGenres)
                {
                    GenreStringList.Add(eachGenre.Name);
                }
                foreach (var genre in genreInEnglish)
                {
                    if (!GenreStringList.Contains(genre.Name))
                    {
                        //if category not exist create
                        await MovieGenreService.AddGenre(new MovieGenreEntity { Name = genre.Name });
                        MovieGenres = MovieGenreService.GetGenreList();
                        var genreModel = MovieGenres.Where(s => s.Name.Contains(genre.Name)).FirstOrDefault();
                        SelectedGenreIds.Add(genreModel.Id.ToString());
                    }
                    else if (GenreStringList.Contains(genre.Name))
                    {
                        //if category exist add to selectedGenreIds
                        var genreModel = MovieGenres.Where(s => s.Name.Contains(genre.Name)).FirstOrDefault();
                        SelectedGenreIds.Add(genreModel.Id.ToString());
                    }
                }
                showStatusMessageApiRequest = "เรียกข่้อมูลสำเร็จ";
            } else
            {
                showStatusMessageApiRequest = "เกิดข้อผิดพลาด เช็คไอดีให้ถูกต้อง";
            }

            ApiLoadSpiner = false;


        }
    }
}

