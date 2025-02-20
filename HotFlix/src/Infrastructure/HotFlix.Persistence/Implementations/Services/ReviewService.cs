using Azure;
using HotFlix.Application.Abstraction.Repositories;
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using HotFlix.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Persistence.Implementations.Services
{
    internal class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;

        public ReviewService(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllAsync(int page,int take)
        {
            IEnumerable<Review> reviews = await _repository.GetAll(null, null, false, false, false, (page - 1) * take, take, "Movie", "User","Ratings").ToListAsync();

            IEnumerable<ReviewDto> reviewDto = reviews.Select(c => new ReviewDto
            {
                Id = c.Id,
               Text=c.Title,
               CreatedAt=c.CreatedAt,
               Rating=c.Ratings.RatingNumber,
               UserName=c.User.UserName,
               MovieName=c.Movie.Name,
            });

            return reviewDto;
        }

        public async Task DeleteAsync(int id)
        {
            Review review = await _repository.GetbyIdAsync(id);
            if (review is null) throw new Exception("Not Found");

            _repository.Delete(review);
            await _repository.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _repository.Count();
        }
    }
}
