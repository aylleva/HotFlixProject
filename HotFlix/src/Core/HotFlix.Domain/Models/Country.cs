

namespace HotFlix.Domain.Models
{
  public class Country:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
