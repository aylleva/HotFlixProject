using Azure.Identity;
using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace HotFlix.Persistence.Implementations.Services
{
    internal class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;

        public CommentService(ICommentRepository repository)
        {
           _repository = repository;
        }
        public async Task<IEnumerable<CommentDto>> GetAllAsync(int page,int take)
        {
            IEnumerable<Comments> comments = await _repository.GetAll(null,null,false,false,false, (page - 1) * take,take,"Movie","User").ToListAsync();

            IEnumerable<CommentDto> commentdto = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Text = c.Description,
                CreatedAt=c.CreatedAt,
                LikeCount=c.LikeCount.Value,
                DislikeCount=c.DisLikeCount.Value,
                UserName=c.User.UserName,
                MovieName=c.Movie.Name
            });

            return commentdto;
        }
        public async Task DeleteAsync(int id)
        {
            Comments comment = await _repository.GetbyIdAsync(id);
            if (comment is null) throw new Exception("Not Found");

            _repository.Delete(comment);
            await _repository.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _repository.Count();
        }
    }
}
