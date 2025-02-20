using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class GetPremiumPlanDto
    {
        public int Id { get; set; }
        public string PlanName { get; set; }
        public decimal Price { get; set; }
        public string Expires { get; set; }
        public string Quality { get; set; }
        public int UserCount {  get; set; }
    }
}
