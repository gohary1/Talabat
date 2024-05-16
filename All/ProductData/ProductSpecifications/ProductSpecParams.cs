using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.ProductSpecifications
{
    public class ProductSpecParams
    {
        public int pageSize { get; set; } = 10;
        public int pageIndex { get; set; } = 2;
        public string? Search { get; set; }
        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? typeId { get; set; }


    }
}
