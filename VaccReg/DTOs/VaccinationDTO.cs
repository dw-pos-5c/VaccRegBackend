using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VaccRegDb;

namespace VaccReg.DTOs
{
    public class VaccinationDTO
    {
        public long RegistrationId { get; set; }
        public DateTime VaccinationDate { get; set; }

        public static VaccinationDTO From(Vaccination input)
        {
            return new VaccinationDTO
            {
                RegistrationId = input.RegistrationId,
                VaccinationDate = input.VaccinationDate,
            };
        }
    }
}
