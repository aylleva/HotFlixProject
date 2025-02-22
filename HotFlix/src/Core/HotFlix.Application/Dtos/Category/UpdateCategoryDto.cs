

using System.ComponentModel.DataAnnotations;

namespace HotFlix.Application.Dtos
{
    public class UpdateCategoryDto
    {
        [MaxLength(200, ErrorMessage = "Category Name Length should not exceed than 200")]
        public string Name { get; set; }
    }
}
