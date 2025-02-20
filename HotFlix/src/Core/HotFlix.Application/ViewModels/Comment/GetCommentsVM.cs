using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels
{
    public class GetCommentsVM
    {
        public int Id {  get; set; }
        public string Description {  get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId {  get; set; }
        public int? LikeCount {  get; set; }
        public int? DislikeCount { get; set; }
        public string UserName {  get; set; }
        public string UserImage {  get; set; }
        public int MovieId { get; set; }
    }
}
