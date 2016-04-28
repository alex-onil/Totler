using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trade_MVC6.Models.Identity.AccountDetails
{
    public class AddressRecord : EntityAbstract
    {
        //public string Addressee { get; set; }
        public string LineOne { get; set; }
        public string LineTwo { get; set; }
        public string CityOrTown { get; set; }
        public string StateOrProvince { get; set; }
        public string ZipOrPostalCode { get; set; }
        public string Country { get; set; }
        }
}
