using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.DTOs;
using ProductApi.Error;
using ProductData.Entites;
using ProductData.ProductSpecifications;
using ProductRepository.Interfaces;

namespace ProductApi.Controllers
{
    
    public class ProductController : BaseController
    {
        private readonly IGenaricRepository<Product, int> _repository;
        private readonly IGenaricRepository<ProductBrand, int> _brandRepository;
        private readonly IGenaricRepository<ProductType, int> _typeRepository;
        private readonly IMapper _mapper;

        public ProductController(IGenaricRepository<Product,int> repository,
                                  IGenaricRepository<ProductBrand, int> BrandRepository,
                                  IGenaricRepository<ProductType, int> TypeRepository,
                                IMapper mapper) 
        { 
            _repository = repository;
            _brandRepository = BrandRepository;
            _typeRepository = TypeRepository;
            _mapper = mapper;
        }
        #region products
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<Product>> GetAllProduct([FromQuery] ProductSpecParams specs)
        {
            var spec = new ProductWithBrandSpecification(specs);
            var products = await _repository.GetAllAsyncWithSpacification(spec);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
        }

        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var spec = new ProductWithBrandSpecification(id);
            var product = await _repository.GetByIdAsyncWithSpacification(spec);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }
        #endregion
        #region Types
        [ProducesResponseType(typeof(ProductType), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("getTypes")]
        public async Task<ActionResult<ProductType>> getTypes()
        {
            var types = await _typeRepository.GetAllAsync();
            if (types == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(types);
        }

        [ProducesResponseType(typeof(ProductType), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("getTypes/{id}")]
        public async Task<ActionResult<ProductType>> getTypeId(int id)
        {
            var types = await _typeRepository.GetByIdAsync(id);
            if (types == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(types);
        }
        #endregion

        #region Brands
        [ProducesResponseType(typeof(ProductBrand), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("getBrands")]
        public async Task<ActionResult<ProductBrand>> getBrand()
        {
            var brands = await _brandRepository.GetAllAsync();
            if (brands == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(brands);
        }

        [ProducesResponseType(typeof(ProductBrand), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("getBrand/{id}")]
        public async Task<ActionResult<ProductType>> getBrandId(int id)
        {
            var brands = await _brandRepository.GetByIdAsync(id);
            if (brands == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(brands);
        } 
        #endregion

    }
}
