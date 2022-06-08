using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Entities;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    public class ProductsController : CustomBaseController // CustomBaseController'ı miras alıyor.
    {

        private readonly IMapper _mapper;
        
        private readonly IProductService _service;

        public ProductsController(IMapper mapper, IProductService service) { 
            _mapper = mapper;
            _service = service;
            
        }

        // Get api/products/GetProductsWithCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {

            return CreateActionResult(await _service.GetProductsCategory());

        }


        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();

            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200,productsDtos));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);

            var productDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));

        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var mappedProduct = _mapper.Map<Product>(productDto);

            var product = await _service.AddAsync(mappedProduct);

            var newProductDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, newProductDto));

        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            var mappedProduct = _mapper.Map<Product>(productDto);

            // Geriye birşey dönmediği için atama yapamıyoruz.
            await _service.UpdateAsync(mappedProduct);

            // NoContentDto ' nun işlevi burda devreye giriyor geriye birşey dönmediğimiz için 2 tane constructor oluşturmustuk burda kullandık.
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(product);

            

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }



    }
}
