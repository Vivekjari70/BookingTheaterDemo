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
    public class MovieRepository:IMovieRepository
    {
        private readonly ApplicationContext _applicationContext;

        public MovieRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<Movie>> Listmovie()
        {
            return await _applicationContext.Movies.ToListAsync();
        }

        public async Task<Movie> Addmovie(Movie movie)
        {
            var userObj = await _applicationContext.Movies.AddAsync(movie);
            await _applicationContext.SaveChangesAsync();
            return userObj.Entity;
        }

        public async Task<Movie> Update(Movie movie)
        {
            var existingMovie = await _applicationContext.Movies.FirstOrDefaultAsync(u => u.MovieId == movie.MovieId);

            if (existingMovie == null)
            {
                return null;
            }

            existingMovie.Title = movie.Title;
            existingMovie.Description = movie.Description;
            existingMovie.ReleaseDate = movie.ReleaseDate;
            existingMovie.Genre = movie.Genre;
            existingMovie.TheaterId = movie.TheaterId;


            await _applicationContext.SaveChangesAsync();

            return existingMovie;
        }

        public async Task<Movie> Delete(Movie movie)
        {
            var existingMovie = await _applicationContext.Movies.FirstOrDefaultAsync(u => u.MovieId == movie.MovieId);

            if (existingMovie == null)
            {
                return null;
            }

            _applicationContext.Movies.Remove(existingMovie);

            await _applicationContext.SaveChangesAsync();

            return existingMovie;
        }

        

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetMovieById(int id)
        {
            return await _applicationContext.Movies.Where(x => x.TheaterId == id).ToListAsync();
        }

        public async Task<List<Movie>> GetMovieByTheater(string title)
        {
            return await _applicationContext.Movies.Include(x => x.Theater).Where(x => x.Title == title).ToListAsync();
        }

        public async Task<Movie> GetMovieByMovieName(string movie_name,int theaterid)
        {
            return await _applicationContext.Movies.FirstOrDefaultAsync(x => x.Title == movie_name && x.TheaterId==theaterid);
        }
    }


}
