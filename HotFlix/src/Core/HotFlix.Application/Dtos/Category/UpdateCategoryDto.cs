

using System.ComponentModel.DataAnnotations;

namespace HotFlix.Application.Dtos
{
    public class UpdateCategoryDto
    {
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
