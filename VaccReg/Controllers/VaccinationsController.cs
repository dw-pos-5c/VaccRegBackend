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
            var minDate = new DateTime(2021, 12,1);
            var maxDate = new DateTime(2021, 12, 20);

            if (date < minDate || date > maxDate)
            {
                return BadRequest("Only Dates between 01.12.2021 - 20.12.2021 are allowed");
            }

            return Ok(vaccinationsService.GetAvailableDates(date));
        }
    }
}
