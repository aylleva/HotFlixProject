
namespace HotFlix.Domain.Models
{
    public class PremiumPlan:BaseEntity
    {
        public string PlanName { get; set; }
        public decimal Price {  get; set; }
        public string Expires {  get; set; }
        public string Quality {  get; set; }

        public ICollection<AppUser> Users { get; set; }
    }
}
