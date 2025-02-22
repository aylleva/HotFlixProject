using HotFlix.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class UpdateMovieDto
    {
        [MaxLength(200, ErrorMessage = "Movie Name Length should not exceed than 200")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Range(1, 11, ErrorMessage = "Rating can not be 0!")]
        public double? Rating { get; set; }
        public int? Premiere { get; set; }
        [MaxLength(30, ErrorMessage = "Movie Name Length should not exceed than 30")]
        public string? RunningTime { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? TrailerUrl { get; set; }
        public IFormFile? VideoUrl { get; set; }
        public int? CategoryId { get; set; }
        public int? CountryId { get; set; }
        public int? DirectorId { get; set; }
        //relational
        public List<Tag>? Tags { get; set; }
        public List<int>? TagIds { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Director>? Directors { get; set; }
        public List<Actor>? Actors { get; set; }
        public List<int>? ActorIds { get; set; }
        public ICollection<Country>? Countries { get; set; }
    }
}
