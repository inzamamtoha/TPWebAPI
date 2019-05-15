using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using TouristPlaceApi.Models;

namespace TouristPlaceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TouristPlaceController : Controller
    {
        private readonly IPlaceService placeService;
        private readonly ICountryService countryService;
        

        public TouristPlaceController(IPlaceService placeService, ICountryService countryService)
        {
            this.placeService = placeService;
            this.countryService = countryService;
            
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Places> Get()
        {
            List<Places> placeModel = new List<Places>();
            placeService.GetAllPlaces().ToList().ForEach(p =>
            {
                Places _place = Mapper.Map<Places>(p);
                placeModel.Add(_place);
            });
            return placeModel;
        }

       
        [HttpGet("Country")]
        public IEnumerable<Places> GetCountry()
        {
            List<Places> placeModel = new List<Places>();
            countryService.GetAllCountries().ToList().ForEach(p =>
            {
                Places _place = new Places
                {
                    Id = p.Id,
                    Name = p.Name
                };
                placeModel.Add(_place);
            });
            return placeModel;
        }

        [HttpGet("Country/{id}")]
        public ActionResult<Places> GetCountry(long id)
        {
            return NotFound();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Places> Get(long id)
        {
            var p = placeService.GetPlace(id);
            if(p == null)
            {
                return NotFound();
            }
            //Mapper.Initialize(cfg => cfg.CreateMap<Place, Places>().ForMember(d => d.Country, p => p.MapFrom(s => s.Country.Name)));
            Places _place = Mapper.Map<Places>(p);
            return _place;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post(Places _place)
        {
        
            Place plc = new Place();
            plc.Name = _place.Name;
            plc.Address = _place.Address;
            plc.Rating = _place.Rating;
            plc.CountryId = _place.CountryId;
            plc.Picture = _place.Picture;
            placeService.InsertPlace(plc);
        }
        
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(long id, Places _place)
        {
            if(id!= _place.Id)
            {
                return BadRequest();
            }
            /*
            Place plc = placeService.GetPlace(id);
            plc = Mapper.Map<Place>(_place);
            placeService.UpdatePlace(plc);
            */
            Place plc = placeService.GetPlace(id);
            plc.Name = _place.Name;
            plc.Address = _place.Address;
            plc.Rating = _place.Rating;
            plc.CountryId = _place.CountryId;
            plc.Picture = _place.Picture;
            placeService.UpdatePlace(plc);
            
            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            if (placeService.GetPlace(id) == null)
            {
                return NotFound();
            }
            placeService.DeletePlace(id);
            return NoContent();
        }
    }
}
