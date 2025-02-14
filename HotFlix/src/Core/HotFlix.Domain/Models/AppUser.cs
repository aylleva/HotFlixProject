using Microsoft.AspNetCore.Identity;


namespace HotFlix.Domain.Models
{
    public class AppUser:IdentityUser
    {
        public string Name {  get; set; }
        public string Surname {  get; set; }
        public bool IsPremium {  get; set; }

        //relational
        public int? PremiumPlanId {  get; set; }
        public PremiumPlan? PremiumPlan { get; set; }
        public string? VerificationCode { get; set; }
        public DateTime? CodeExpiryTime { get; set; }
        public int WatchedFilms {  get; set; }
        public ICollection<Comments>? Comments { get; set; }
        public ICollection<FavoriteMovies>? FavoriteMovies { get; set; }

    }
}
