using Microsoft.AspNetCore.Components;
using MpStream.Models;
using MpStream.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MpStream.Pages.Member.Movies
{
    public partial class Comments : ComponentBase
    {
        [Parameter]
        public MovieEntity MovieEntity { get; set; }
        [Inject]
        public CommentService CommentService { get; set; }

        public MovieComment MovieComment { get; set; } = new MovieComment();
        public List<MovieComment> MovieComents { get; set; } = new List<MovieComment>();
        public bool IsDefaultComment { get; set; } = true;
        public bool IsFaceBookComment { get; set; } = false;
        public string Message { get; set; }
        protected override async Task OnInitializedAsync()
        {
            MovieComents = await CommentService.DisplayComments(MovieEntity.Id);
        }
        protected override async Task OnParametersSetAsync()
        {
            MovieComents = await CommentService.DisplayComments(MovieEntity.Id);
        }

        async Task SubmitComment()
        {
            MovieComment.MovieEntityId = MovieEntity.Id;
            var result = await CommentService.AddMovieComment(MovieComment);
            if (result)
            {
                MovieComment = new MovieComment();
                MovieComents = await CommentService.DisplayComments(MovieEntity.Id);
                Message = "ส่งข้อความสำเร็จ";
            } else
            {
                MovieComment = new MovieComment();
                Message = "เกิดข้อผิดพลาด";
            }
        }
    }
}
