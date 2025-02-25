using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class PaginatedItemDto<T>
    {
        public int CurrectPage {  get; set; }
        public double TotalPage {  get; set; }
        public Task<IEnumerable<T>> Items { get; set; } 
        public string? Search {  get; set; }
    }
}
