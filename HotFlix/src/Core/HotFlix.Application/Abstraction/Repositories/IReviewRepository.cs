using HotFlix.Application.Abstraction.Repositories.Generic;
using HotFlix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Application.Abstraction.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
    }
}
