
using System.ComponentModel.DataAnnotations;


namespace HotFlix.Application.ViewModels
{
    public class UpdateUserVM
    {
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }
        [MinLength(3)]
        [MaxLength(40)]
        public string Surname { get; set; }
        [MinLength(3)]
        [MaxLength(256)]
        public string UserName { get; set; }
        [MinLength(6)]
        [MaxLength(256)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    
    }
}
