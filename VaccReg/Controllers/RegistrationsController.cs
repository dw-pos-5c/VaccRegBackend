using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VaccReg.Services;

using VaccRegDb;

namespace VaccReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly RegistrationsService registrationsService;

        public RegistrationsController(RegistrationsService registrationsService)
        {
            this.registrationsService = registrationsService;
        }
    }
}
