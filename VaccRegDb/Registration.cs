using System;
using System.Collections.Generic;

#nullable disable

namespace VaccRegDb
{
    public partial class Registration
    {
        public long Id { get; set; }
        public long SocialSecurityNumber { get; set; }
        public long PinCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Vaccination Vaccination { get; set; }
    }
}
