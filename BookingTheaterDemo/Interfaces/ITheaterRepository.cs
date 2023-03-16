using BookingTheaterDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTheaterDemo.Interfaces
{
    public interface ITheaterRepository : ICommonRepository
    {
        Task<List<Theater>> Listtheater();
        Task<Theater> Addtheater(Theater theater);
        Task<Theater> Update(Theater theater);
        Task<Theater> Delete(Theater theater);
        Task<List<Theater>> GetTheaterById(int id);
    }
}
