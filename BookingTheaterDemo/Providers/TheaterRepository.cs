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
    public class TheaterRepository : ITheaterRepository
    {
        private readonly ApplicationContext _applicationContext;

        public TheaterRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<List<Theater>> Listtheater()
        {
            return await _applicationContext.Theaters.ToListAsync();
        }
        public async Task<Theater> Addtheater(Theater theater)
        {
            var userObj = await _applicationContext.Theaters.AddAsync(theater);
            await _applicationContext.SaveChangesAsync();
            return userObj.Entity;
        }

        public async Task<Theater> Update(Theater theater)
        {
            var existingTheater = await _applicationContext.Theaters.FirstOrDefaultAsync(u => u.TheaterId == theater.TheaterId);

            if (existingTheater == null)
            {
                return null;
            }

            existingTheater.Name = theater.Name;
            existingTheater.Address = theater.Address;
            existingTheater.Capacity = theater.Capacity;
            existingTheater.CityId = theater.CityId;

            await _applicationContext.SaveChangesAsync();

            return existingTheater;
        }

        public async Task<Theater> Delete(Theater theater)
        {
            var existingTheater = await _applicationContext.Theaters.FirstOrDefaultAsync(u => u.TheaterId == theater.TheaterId);

            if (existingTheater == null)
            {
                return null;
            }

            _applicationContext.Theaters.Remove(existingTheater);

            await _applicationContext.SaveChangesAsync();

            return existingTheater;
        }

        

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Theater>> GetTheaterById(int id)
        {
            return await _applicationContext.Theaters.Where(x => x.CityId == id).ToListAsync();
        }

       
    }
}
