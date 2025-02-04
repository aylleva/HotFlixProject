

namespace HotFlix.Domain.Models
{
    public class Actor:BaseEntity
    {
        public string FullName {  get; set; }

        public string Image {  get; set; }
        public int Age {  get; set; }
        public int MovieCount {  get; set; }
        public ICollection<MovieActors> MovieActors { get; set; }
    }
}
