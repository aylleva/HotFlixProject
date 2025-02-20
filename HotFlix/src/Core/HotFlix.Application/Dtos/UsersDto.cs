using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos
{
    public class UsersDto
    {
        public string Id {  get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Image {  get; set; }
        public string? PremiumPlan {  get; set; }
        public int CommentCount {  get; set; }
        public int ReviewCount {  get; set; }
        public bool Status {  get; set; }

    }
}
