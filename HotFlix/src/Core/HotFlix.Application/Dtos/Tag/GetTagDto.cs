using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Dtos;
    public class GetTagDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MovieCount { get; set; }
    }

