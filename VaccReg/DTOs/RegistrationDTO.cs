using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VaccRegDb;

namespace VaccReg.DTOs
{
    public class RegistrationDTO
    {
        public long Id { get; set; }
        public long SocialSecurityNumber { get; set; }
        public long PinCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static RegistrationDTO From(Registration input)
        {
            return new RegistrationDTO
            {
                Id = input.Id,
                SocialSecurityNumber = input.SocialSecurityNumber,
                PinCode = input.PinCode,
                FirstName = input.FirstName,
                LastName = input.LastName,
            };
        }
    }
}
