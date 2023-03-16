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
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListTheater()
        {
            var data = await _cityRepository.Listcity();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCityById(int id)
        {
            var cityDetails = await _cityRepository.GetCityById(id);
            if (cityDetails == null)
            {
                return NotFound();
            }
            return Ok(cityDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Addtheater([FromBody] CityDto cityDto)
        {
            var city = new City()
            {
                Name = cityDto.Name,
                Population = cityDto.Population,
            };

            await _cityRepository.Addcity(city);

            return Ok(city);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Updatecity(int id, [FromBody] CityDto cityDto)
        {
            var city = new City()
            {
                CityId = id,
                Name = cityDto.Name,
                Population = cityDto.Population
            };

            var updatedCity = await _cityRepository.Update(city);

            if (updatedCity == null)
            {
                return NotFound();
            }

            return Ok(updatedCity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var CityToDelete = new City() { CityId = id };

            var deletedCity = await _cityRepository.Delete(CityToDelete);

            if (deletedCity == null)
            {
                return NotFound();
            }

            return Ok(deletedCity);
        }
    }
}
