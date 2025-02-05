using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Domain.Models
{
    public class Contact:BaseEntity
    {
        public string Name {  get; set; }
        public string Email { get; set; }
        public string PartnerShip {  get; set; }
        public string Message {  get; set; }
    }
}
