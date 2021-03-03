using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class PublisherService : IPublisherService
    {
        private BookStoreDbContext dbContext;

        public PublisherService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int EditPublisher(Publisher publisher)
        {
            dbContext.Entry(publisher).State = EntityState.Modified;
            return dbContext.SaveChanges();
            
        }

        public IList<Publisher> GetPublishers()
        {
            return dbContext.Publishers.AsNoTracking().ToList();
        }
    }
}
