using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccRegDb;

namespace VaccReg.Services
{
    public class VaccinationsService
    {
        private readonly VaccRegContext db;

        public VaccinationsService(VaccRegContext db)
        {
            this.db = db;
        }

        public List<Vaccination> GetAll()
        {
            return db.Vaccinations.ToList();
        }

        public List<DateTime> GetAvailableDates(DateTime date)
        {
            List<DateTime> availVaccinations = new();

            var notAvailList = db.Vaccinations.Where(x => x.VaccinationDate.Date == date.Date).ToList();
            var start = date.Date.AddHours(8);

            for (DateTime current = start; current < start.AddHours(3); current = current.AddMinutes(15))
            {
                if (!notAvailList.Any(x => x.VaccinationDate.Equals(current)))
                {
                    availVaccinations.Add(current);
                }
            }

            return availVaccinations;
        }
    }
}
