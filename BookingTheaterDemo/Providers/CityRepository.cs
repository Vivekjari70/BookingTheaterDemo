using BookingTheaterDemo.Data;
using BookingTheaterDemo.Interfaces;
using BookingTheaterDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTheaterDemo.Providers
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationContext _applicationContext;

        public CityRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<List<City>> Listcity()
        {
            return await _applicationContext.Cities.ToListAsync();
        }
        public async Task<City> Addcity(City city)
        {
            var userObj = await _applicationContext.Cities.AddAsync(city);
            await _applicationContext.SaveChangesAsync();
            return userObj.Entity;
        }
        public async Task<City> Update(City city)
        {
            var existingCity = await _applicationContext.Cities.FirstOrDefaultAsync(u => u.CityId == city.CityId);

            if (existingCity == null)
            {
                return null;
            }

            existingCity.Name = city.Name;
            existingCity.Population = city.Population;

            await _applicationContext.SaveChangesAsync();

            return existingCity;
        }
        public async Task<City> Delete(City city)
        {
            var existingCity = await _applicationContext.Cities.FirstOrDefaultAsync(u => u.CityId == city.CityId);

            if (existingCity == null)
            {
                return null;
            }

            _applicationContext.Cities.Remove(existingCity);

            await _applicationContext.SaveChangesAsync();

            return existingCity;
        }
        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<City> GetCityById(int id)
        {
            return await _applicationContext.Cities.Include(c => c.Theaters).FirstOrDefaultAsync(x => x.CityId == id);
        }
    }
}
