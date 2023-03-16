using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTheaterDemo.Interfaces
{
    public interface ICommonRepository
    {
        Task SaveChanges();
    }
}
