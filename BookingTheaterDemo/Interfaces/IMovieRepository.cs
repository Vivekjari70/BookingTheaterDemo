using BookingTheaterDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTheaterDemo.Interfaces
{
    public interface IMovieRepository:ICommonRepository
    {
        Task<Movie> Addmovie(Movie movie);
        Task<List<Movie>> Listmovie();
        Task<Movie> Update(Movie movie);
        Task<Movie> Delete(Movie movie);

        Task<List<Movie>> GetMovieById(int id);

        Task<List<Movie>> GetMovieByTheater(string title);
        Task<Movie> GetMovieByMovieName(string movie_name,int theaterid);
    }
}
