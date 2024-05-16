using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Entites.Idintity
{
    public class Address
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [ForeignKey("AppUserid")]
        public string AppUserId { get; set; }
        public AppUser AppUserid { get; set; }
    }
}
