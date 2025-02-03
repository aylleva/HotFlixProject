

namespace HotFlix.Domain.Models
{
    public class Actor:BaseEntity
    {
        public string FullName {  get; set; }
        public ICollection<MovieActors> MovieActors { get; set; }
    }
}
