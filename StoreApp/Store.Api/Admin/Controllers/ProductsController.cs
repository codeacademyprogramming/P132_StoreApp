using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Api.Admin.Dtos.ProductDtos;
using Store.Core.Entities;
using Store.Data.DAL;

namespace Store.Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(StoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("")]
        public IActionResult Create(ProductPostDto postDto)
        {
            if (!_context.Categories.Any(x => x.Id == postDto.CategoryId))
                return BadRequest(new { error = new { field = "CategoryId", message = "Catgory not found!" } });

            if (_context.Products.Any(x => x.Name == postDto.Name)) 
                return BadRequest(new { error = new { field = "Name", message = "Product already exist!" } });
        

            Product product = _mapper.Map<Product>(postDto);

            _context.Products.Add(product);
            _context.SaveChanges();

            return StatusCode(201, product);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.Include(x=>x.Category).FirstOrDefault(x => x.Id == id);

            if (product == null) return NotFound();

            ProductGetDto productDto = _mapper.Map<ProductGetDto>(product);

            return Ok(productDto);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var products = _context.Products.Skip((page - 1) * 4).Take(4).ToList();

            var productDtos = products.Select(x => new ProductListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                CostPrice = x.CostPrice,
                SalePrice = x.SalePrice,
                DiscountPercent = x.DiscountPercent
            }).ToList();

            return Ok(productDtos);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id,ProductPostDto postDto)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null) return NotFound();

            if(product.CategoryId!=postDto.CategoryId && !_context.Categories.Any(x=>x.Id == postDto.CategoryId))
                return BadRequest(new { error = new { field = "CategoryId", message = "Catgory not found!" } });


            if (product.Name!=postDto.Name && _context.Products.Any(x =>x.Id!=id && x.Name == postDto.Name))
                return BadRequest(new { error = new { field = "Name", message = "Product already exist!" } });


            product.Name = postDto.Name;
            product.CategoryId = postDto.CategoryId;
            product.CostPrice = postDto.CostPrice;
            product.SalePrice = postDto.SalePrice;
            product.DiscountPercent = postDto.DiscountPercent;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }


    }
}
