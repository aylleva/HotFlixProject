
namespace HotFlix.Domain.Models
{
    public class Movie:BaseEntity
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        public double Rating {  get; set; }
        public int Premiere {  get; set; }
        public string RunningTime{  get; set; }
        public string Image {  get; set; }
        public string TrailerUrl { get; set; }

        //relational
        public ICollection<MovieTags> MovieTags { get; set; }
        public Category Category { get; set; }
        public Director Director { get; set; }
        public ICollection<MovieActors> MovieActors { get; set; }
        public Country Country { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<MovieVideos> Videos { get; set; }
       

    }
}
