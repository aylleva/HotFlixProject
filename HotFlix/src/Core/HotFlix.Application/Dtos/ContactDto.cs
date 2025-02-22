

using HotFlix.Domain.Models;

namespace HotFlix.Application.Dtos
{
    public class ContactDto
    {
        public int? Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PartnerShipName { get; set; }
        public string? Message { get; set; }
        
    }
}
