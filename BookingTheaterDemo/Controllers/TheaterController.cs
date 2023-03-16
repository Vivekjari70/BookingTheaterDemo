using BookingTheaterDemo.Dtos;
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
    public class TheaterController : ControllerBase
    {
        private readonly ITheaterRepository _theaterRepository;

        public TheaterController(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListTheater()
        {
            var data = await _theaterRepository.Listtheater();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTheaterById(int id)
        {
            var theaterDetails = await _theaterRepository.GetTheaterById(id);
            if (theaterDetails == null)
            {
                return NotFound();
            }
            return Ok(theaterDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Addtheater([FromBody] TheaterDto theaterDto)
        {
            var theater = new Theater()
            {
                Name = theaterDto.Name,
                Address = theaterDto.Address,
                Capacity = theaterDto.Capacity,
                CityId = theaterDto.CityId
            };

            await _theaterRepository.Addtheater(theater);

            return Ok(theater);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updatetheater(int id, [FromBody] TheaterDto theaterDto)
        {
            var theater = new Theater()
            {
                TheaterId = id,
                Name = theaterDto.Name,
                Address = theaterDto.Address,
                Capacity = theaterDto.Capacity,
                CityId = theaterDto.CityId
            };

            var updatedtheater = await _theaterRepository.Update(theater);

            if (updatedtheater == null)
            {
                return NotFound();
            }

            return Ok(updatedtheater);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var TheaterToDelete = new Theater() { TheaterId = id };

            var deletedTheater = await _theaterRepository.Delete(TheaterToDelete);

            if (deletedTheater == null)
            {
                return NotFound();
            }

            return Ok(deletedTheater);
        }

    }
}
