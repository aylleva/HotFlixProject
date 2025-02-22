
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
        public string VideoUrl {  get; set; }
        public int CategoryId {  get; set; }
        public int CountryId {  get; set; }
        public int DirectorId {  get; set; }
        public bool Status {  get; set; }
        public int Views {  get; set; }
        //relational
        public List<MovieTags> MovieTags { get; set; }
        public Category Category { get; set; }
        public Director Director { get; set; }
        public List<MovieActors> MovieActors { get; set; }
        public Country Country { get; set; }
        public ICollection<Comments> Comments { get; set; }
        public ICollection<Season>? Seasons { get; set; }
        public ICollection<Review>? Reviews { get; set; }

    }
}
