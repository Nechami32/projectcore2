using OurApi.Models;
using System.Collections.Generic;
using System.Linq;
using OurApi.Controllers;
using OurApi.Services;


namespace OurApi.Interfaces
{
    public interface Iglass
    {
        List<Glasse> GetAll(int UserId);

        Glasse  Get(int id);

        int Add(Glasse g,int UserId);

        void Delete(int id);

        void Update(Glasse g,int UserId);

        int Count { get; }
    }
}
