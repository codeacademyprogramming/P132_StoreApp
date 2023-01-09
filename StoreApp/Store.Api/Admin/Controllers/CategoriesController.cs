using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Admin.Dtos.CategoryDtos;
using Store.Core.Entities;
using Store.Data.DAL;

namespace Store.Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly StoreDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(StoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("")]
        public IActionResult Create(CategoryPostDto postDto)
        {
            if (_context.Categories.Any(x => x.Name == postDto.Name))
            {
                ModelState.AddModelError("Name", "Category already created!");
                return BadRequest(ModelState);
            }

            Category category = _mapper.Map<Category>(postDto);

            _context.Categories.Add(category);
            _context.SaveChanges();

            return StatusCode(201, category);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return NotFound();

            CategoryGetDto categoryDto = _mapper.Map<CategoryGetDto>(category);

            return Ok(categoryDto);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var categories = _context.Categories.Skip((page - 1) * 4).Take(4).ToList();

            var categoryDtos = _mapper.Map<List<CategoryListItemDto>>(categories);

            return Ok(categoryDtos);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id,CategoryPostDto postDto)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null) return NotFound();

            if (_context.Categories.Any(x => x.Id != id && x.Name == postDto.Name)) return BadRequest();

            category.Name = postDto.Name;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
