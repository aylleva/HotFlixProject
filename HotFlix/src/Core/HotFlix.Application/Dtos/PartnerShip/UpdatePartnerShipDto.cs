using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class UpdatePartnerShipDto
    {
        [MaxLength(100, ErrorMessage = "PartnerShip Name Length should not exceed than 100")]
        public string Name { get; set; }
    }
}
