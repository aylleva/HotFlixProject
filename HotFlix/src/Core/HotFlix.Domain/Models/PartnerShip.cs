using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Domain.Models
{
   public  class PartnerShip:BaseEntity
    {
        public string Name {  get; set; }
        public ICollection<JobContact>? Contacts { get; set; }
    }
}
