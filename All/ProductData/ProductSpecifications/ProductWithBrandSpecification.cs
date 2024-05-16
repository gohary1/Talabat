using ProductData.Entites;
using ProductData.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.ProductSpecifications
{
    public class ProductWithBrandSpecification : BaseSpacificaton<Product, int>
    {
        public ProductWithBrandSpecification(ProductSpecParams specs) :
            base(p =>
                (string.IsNullOrEmpty(specs.Search) || p.Name.ToLower().Contains(specs.Search.ToLower())) &&
                (!specs.brandId.HasValue || p.BrandId == specs.brandId) &&
                (!specs.typeId.HasValue || p.TypeId == specs.typeId))
            //    !string.IsNullOrEmpty(specs.Search) || p.Name.ToLower().Contains(specs.Search.ToLower()) &&
            //    (specs.brandId.HasValue && p.BrandId == specs.brandId) &&
            //    (specs.typeId.HasValue && p.TypeId == specs.typeId))
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
            
                if (specs.sort == "priceAsc") { AddOrderBy(p => p.Price); }
                else if (specs.sort == "priceDesc") { AddOrderByDesc(p => p.Price); }
                else if (specs.sort == null) { AddOrderBy(p => p.Name); }
            //applyPagination((specs.pageIndex - 1) * specs.pageSize, specs.pageSize);
        }
        public ProductWithBrandSpecification(int Id):base(p=>p.id==Id)
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }
    }
}
