using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.ViewModels
{
   public  class DetailVM
    {
        public ICollection<Movie> RelatedMovies { get; set; }
        public Movie Movie { get; set; }
        public ICollection<GetCommentsVM>? Comments { get; set; }
        public CreateCommentVM? CommentVM { get; set; }
        public int? SezonId {  get; set; }
        public int? SerieId {  get; set; }
        
    }
}
