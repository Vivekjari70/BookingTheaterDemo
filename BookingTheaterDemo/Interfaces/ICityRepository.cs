using BookingTheaterDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTheaterDemo.Interfaces
{
    public interface ICityRepository : ICommonRepository
    {
        Task<City> Addcity(City city);
        Task<List<City>> Listcity();
        Task<City> Update(City city);
        Task<City> Delete(City city);
        Task<City> GetCityById(int id);
    }
}
