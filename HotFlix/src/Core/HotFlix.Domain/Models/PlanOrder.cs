using HotFlix.Domain.Models;
namespace HotFlix.Domain
{
    public class PlanOrder:BaseEntity
    {
        public string Name {  get; set; }
        public string Surname {  get; set; }
        public string UserId {  get; set; }
        public AppUser? User { get; set; }
        public int PremiumPlanId {  get; set; }
        public PremiumPlan? PremiumPlanPlan { get; set; }
        public bool? Status {  get; set; }
        public decimal Price {  get; set; }
    }
}
