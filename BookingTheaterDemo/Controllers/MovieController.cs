using BookingTheaterDemo.Dtos;
using BookingTheaterDemo.Helpers;
using BookingTheaterDemo.Interfaces;
using BookingTheaterDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTheaterDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        [HttpGet]
        public async Task<IActionResult> ListMovie()
        {
            var data = await _movieRepository.Listmovie();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
             var userDetails = await _movieRepository.GetMovieById(id);
            return Ok(userDetails);
        }
        [HttpGet("movie-by-theater")]
        public async Task<IActionResult> GetMovieByTheater(string title)
        {
            var userDetails = await _movieRepository.GetMovieByTheater(title);

            var theatreDetails = userDetails.Select(x =>new
            {
                title = x.Title,
                description = x.Description,
                theatreName = x.Theater.Name,
                releaseDate = x.ReleaseDate

            });

            return Ok(theatreDetails);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieDto movieDto)
        {
            var MovieDetails = await _movieRepository.GetMovieByMovieName(movieDto.Title , movieDto.TheaterId);

            if ( MovieDetails == null)
            {
                return BadRequest(new ResponseModel
                {
                    ErrorCode = "U001",
                    Message = ErrorMessage.U001
                });
            }

            var movie = new Movie()
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                ReleaseDate = movieDto.ReleaseDate,
                Genre = movieDto.Genre,
                TheaterId = movieDto.TheaterId
            };

            await _movieRepository.Addmovie(movie);

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieDto movieDto)
        {
            var movie = new Movie()
            {
                MovieId = id,
                Title = movieDto.Title,
                Description = movieDto.Description,
                ReleaseDate = movieDto.ReleaseDate,
                Genre = movieDto.Genre,
                TheaterId = movieDto.TheaterId
            };

            var updatedmovie = await _movieRepository.Update(movie);

            if (updatedmovie == null)
            {
                return NotFound();
            }

            return Ok(updatedmovie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var MovieToDelete = new Movie() { MovieId = id };

            var deletedMovie = await _movieRepository.Delete(MovieToDelete);

            if (deletedMovie == null)
            {
                return NotFound();
            }

            return Ok(deletedMovie);
        }
    }
}
