using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.Database.Repository
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly IList<Review> _reviews;
        public ReviewRepository()
        {
            _reviews = new List<Review>
            {
                new Review { Id=1, Route=new Route{ Id=1}, Date=DateTime.Now.AddDays(-1).AddHours(-4),
                Score=5, Description="Super trasa"},
                new Review { Id=2, Route=new Route{ Id=1}, Date=DateTime.Now.AddDays(-2).AddHours(-3),
                Score=4, Description="Polecam"},
                new Review { Id=3, Route=new Route{ Id=1}, Date=DateTime.Now.AddDays(-4).AddHours(2),
                Score=4, Description="Warto..."},
                new Review { Id=4, Route=new Route{ Id=2}, Date=DateTime.Now.AddDays(-1).AddHours(-4),
                Score=3, Description="Srednio"},
                new Review { Id=5, Route=new Route{ Id=2}, Date=DateTime.Now.AddDays(-2).AddHours(-3),
                Score=2, Description="Słabo.."},
                new Review { Id=6, Route=new Route{ Id=2}, Date=DateTime.Now.AddDays(-4).AddHours(2),
                Score=3}
            };
        }
        public void Add(Review entity)
        {
            if (_reviews.Count > 0)
            {
                entity.Id = _reviews.Max(r => r.Id) + 1;
            }
            else
            {
                entity.Id = 1;
            }
            _reviews.Add(entity);
        }

        public void Delete(int id)
        {
            _ = _reviews.Remove(Get(id));
        }

        public Review Get(int id)
        {
            {
                try
                {
                    return _reviews.SingleOrDefault(r => r.Id == id);
                }
                catch
                {
                    throw new InvalidOperationException($"Unique key violaton: Review ID:{id}");
                }
            }
        }

        public IEnumerable<Review> GetAll()
        {
            return _reviews;
        }

        public void Update(int id, Review review)
        {
            var existingReview = Get(id);
            if (existingReview == null)
            {
                throw new RecordNotFoundException($"Review ID:{id} not found in repository");
            }
            existingReview.Route = review.Route;
            existingReview.User = review.User;
            existingReview.Date = review.Date;
            existingReview.Description = review.Description;
            existingReview.Score = review.Score;    
        }
    }
}
