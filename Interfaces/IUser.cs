using Microsoft.AspNetCore.Mvc;
using OurApi.Models;
using OurApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace OurApi.Interfaces
{
    public interface Iuser
    {
        List<User> GetAll();
       User Get(int id);


        public int ExistUser(string name, string password);
        void Add(User newuser);

       // void Delete(int id);

        void Update(User user);

        int Count { get; }
    }
}
