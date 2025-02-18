using HotFlix.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class UpdateMovieDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public int Premiere { get; set; }
        public string RunningTime { get; set; }
        public IFormFile Image { get; set; }
        public string TrailerUrl { get; set; }
        public string VideoUrl { get; set; }
        public int CategoryId { get; set; }
        public int CountryId { get; set; }
        public int DirectorId { get; set; }
        //relational
        public ICollection<Tag> Tags { get; set; }
        public ICollection<int> TagIds { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Director>? Directors { get; set; }
        public ICollection<Actor>? Actors { get; set; }
        public ICollection<int>? ActorIds { get; set; }
        public ICollection<Country> Countries { get; set; }
    }
}
