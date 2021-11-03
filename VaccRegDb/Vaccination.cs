using System;
using System.Collections.Generic;

#nullable disable

namespace VaccRegDb
{
    public partial class Vaccination
    {
        public long Id { get; set; }
        public long RegistrationId { get; set; }
        public string VaccinationDate { get; set; }

        public virtual Registration Registration { get; set; }
    }
}
