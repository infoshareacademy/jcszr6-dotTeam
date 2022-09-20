﻿using Microsoft.EntityFrameworkCore;
using PlanAndRide.BusinessLogic;
using PlanAndRide.BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanAndRide.Database.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly PlanAndRideContext _context;

        public ReviewRepository(PlanAndRideContext context)
        {
            _context = context;
            //_reviews = new List<Review>
            //{
            //    new Review { Id=1, Route=new Route{ Id=1}, Date=DateTime.Now.AddDays(-1).AddHours(-4),
            //    Score=5, Description="Super trasa"},
            //    new Review { Id=2, Route=new Route{ Id=1}, Date=DateTime.Now.AddDays(-2).AddHours(-3),
            //    Score=4, Description="Polecam"},
            //    new Review { Id=3, Route=new Route{ Id=1}, Date=DateTime.Now.AddDays(-4).AddHours(2),
            //    Score=4, Description="Warto..."},
            //    new Review { Id=4, Route=new Route{ Id=2}, Date=DateTime.Now.AddDays(-1).AddHours(-4),
            //    Score=3, Description="Srednio"},
            //    new Review { Id=5, Route=new Route{ Id=2}, Date=DateTime.Now.AddDays(-2).AddHours(-3),
            //    Score=2, Description="Słabo.."},
            //    new Review { Id=6, Route=new Route{ Id=2}, Date=DateTime.Now.AddDays(-4).AddHours(2),
            //    Score=3}
            //};

        }
        public async Task Add(Review entity)
        {
            await _context.Reviews.AddAsync(entity);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            //var review = await _context.Reviews.SingleOrDefaultAsync(r=>r.Id==id);
            //_context.Remove(review);
            //_context.SaveChanges();
            throw new NotImplementedException();
        }

        public async Task<Review> Get(int id)
        {
            //try
            //{
            //    return await _context.Reviews.SingleOrDefaultAsync(r => r.Id == id);
            //}
            //catch
            //{
            //    throw new InvalidOperationException($"Unique key violation: Review ID:{id}");
            //}
            throw new NotImplementedException();
        }


        //public async Task<IEnumerable<Review>> GetByRoute(int id)
        //{
        //    return await _context.Reviews.Where(r => r.Route.Id == id).ToListAsync();
        //}

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Route)
                .ToListAsync();
        }

        public async Task Update(int id, Review review)
        {
            //var existingReview = await _context.Reviews.SingleOrDefaultAsync(r=>r.Id==id);
            //if (existingReview == null)
            //{
            //    throw new RecordNotFoundException($"Review ID:{id} not found in repository");
            //}
            //existingReview.Route = review.Route;
            //existingReview.User = review.User;
            //existingReview.Date = review.Date;
            //existingReview.Description = review.Description;
            //existingReview.Score = review.Score;

            //_context.SaveChanges();
            throw new NotImplementedException();
        }
    }
}
