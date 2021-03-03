using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public class FakeUserService : IUserService
    {
        private List<User> users = new List<User>
        {
            new User{UserName="selin",Name="Selin",Password ="123", Role=new Role{Name="Admin"} },
            new User{UserName="cagri",Name="Çağrı", Password="123", Role=new Role{Name="StandartUser"} }
        };

        public User ValidUser(string UserName, string password)
        {
            return users.FirstOrDefault(u => u.UserName == UserName && u.Password == password);
        }
    }
}
