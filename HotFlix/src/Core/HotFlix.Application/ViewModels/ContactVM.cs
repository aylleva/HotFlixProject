using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels
{
    public class ContactVM
    {
        [MinLength(3)]
        public string Name {  get; set; }
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email {  get; set; }
        [MaxLength (100)]
        public string PartnerShip {  get; set; }
        [MaxLength(1000)]
        public string Message {  get; set; }

    }
}
