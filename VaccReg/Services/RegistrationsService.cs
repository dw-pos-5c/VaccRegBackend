using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using VaccRegDb;

namespace VaccReg.Services
{
    public class RegistrationsService
    {
        private readonly VaccRegContext db;

        public RegistrationsService(VaccRegContext db)
        {
            this.db = db;
        }

        public Registration CheckRegistration(long ssn, long pin)
        {
            return db.Registrations.FirstOrDefault(x => x.SocialSecurityNumber == ssn && x.PinCode == pin);
        }
    }
}
