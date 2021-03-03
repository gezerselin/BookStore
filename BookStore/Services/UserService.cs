using BookStore.Data;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class UserService : IUserService
    {
        private BookStoreDbContext dbContext;
        public UserService(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public User ValidUser(string UserName, string password)
        {
            return dbContext.User.FirstOrDefault(u => u.UserName == UserName && u.Password == password);
        }
    }
}
