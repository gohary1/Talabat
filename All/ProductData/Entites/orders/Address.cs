using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Entites.orders
{
    public class Address
    {
        public Address()
        {
            
        }
        public Address(string fname, string lname, string city, string street, string country)
        {
            Fname = fname;
            Lname = lname;
            City = city;
            Street = street;
            Country = country;
        }

        public string Fname { get; set; }
        public string Lname { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
    }
}
