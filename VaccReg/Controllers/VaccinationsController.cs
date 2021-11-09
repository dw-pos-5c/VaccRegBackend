using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using VaccReg.DTOs;
using VaccReg.Services;

using VaccRegDb;

namespace VaccReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationsController : ControllerBase
    {
        private readonly VaccinationsService vaccinationsService;

        public VaccinationsController(VaccinationsService vaccinationsService)
        {
            this.vaccinationsService = vaccinationsService;
        }

        [HttpGet]
        public IActionResult GetVaccinations()
        {
            return Ok(vaccinationsService.GetAll().Select(VaccinationDTO.From));
        }


        [HttpGet("available")]
        public IActionResult GetVaccinations(DateTime date)
        {
            var minDate = new DateTime(2021, 12,1, 0, 0, 0);
            var maxDate = new DateTime(2021, 12, 20, 23, 59, 59);

            if (date.Millisecond < minDate.Millisecond || date.Millisecond > maxDate.Millisecond)
            {
                return BadRequest("Only Dates between 01.12.2021 and 20.12.2021 are allowed");
            }

            return Ok(vaccinationsService.GetAvailableDates(date));
        }

        [HttpPost]
        public IActionResult AddVaccination([FromBody] VaccinationDTO vaccination)
        {
            var newVacc = vaccinationsService.AddVaccination(vaccination);

            if (newVacc == null)
            {
                return BadRequest("Invalid Vaccination!");
            }

            return Ok(newVacc);
        }
    }
}
