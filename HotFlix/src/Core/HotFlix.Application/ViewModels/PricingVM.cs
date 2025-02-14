using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels
{
   public class PricingVM
    {
        public ICollection<PremiumPlan>? Plans { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PlanId { get; set; }


    }
}
