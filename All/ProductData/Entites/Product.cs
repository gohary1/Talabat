using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Entites
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        
        public ProductType Type { get; set; }
        public int TypeId { get; set; }
        public ProductBrand Brand { get; set; }
        public int BrandId { get; set; }
    }
}
