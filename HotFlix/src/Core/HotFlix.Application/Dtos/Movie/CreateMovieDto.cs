using HotFlix.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HotFlix.Application.Dtos
{
    public class CreateMovieDto
    {
        [MaxLength(200, ErrorMessage = "Movie Name Length should not exceed than 200")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description can not be empty!")]
        public string Description { get; set; }
        [Range(1, 11, ErrorMessage = "Rating can not be 0!")]
        [Required(ErrorMessage = "Rating can not be empty!")]
        public double Rating { get; set; }
        [Required(ErrorMessage = "Premiere can not be empty!")]
        public int Premiere { get; set; }
        [MaxLength(30, ErrorMessage = "Movie Name Length should not exceed than 30")]
        [Required(ErrorMessage = "Running Time can not be empty!")]
        public string RunningTime { get; set; }
        [Required(ErrorMessage = "Image can not be empty!")]
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Trailer can not be empty!")]
        public IFormFile TrailerUrl { get; set; }
        [Required(ErrorMessage = "Video can not be empty!")]
        public IFormFile VideoUrl { get; set; }
        [Required(ErrorMessage = "Category can not be empty!")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Country can not be empty!")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Director can not be empty!")]
        public int DirectorId { get; set; }
        //relational
        public List<Tag>? Tags { get; set; }
        public ICollection<int>? TagIds { get; set; }  
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Director>? Directors { get; set; }
        public List<Actor>? Actors { get; set; }
        public ICollection<int>? ActorIds { get; set; }
        public ICollection<Country>? Countries { get; set; }
    }
}
