using HotFlix.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace HotFlix.Application.ViewModels
{
    public class CreateReviewVm
    {
        [MaxLength(1000)]
        public string Title {  get; set; }
        public int RatingId {  get; set; }
    }
}
