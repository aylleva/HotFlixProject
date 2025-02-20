using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class UpdatePremiumPlanDto
    {
        [MaxLength(50)]
        public string? PlanName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Price can not be 0!")]
        public decimal? Price { get; set; }
        [MaxLength(100)]
        public string? Expires { get; set; }
        [MaxLength(100)]
        public string? Quality { get; set; }
    }
}
