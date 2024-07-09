using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTOs;
using WebApplication1.Repositries;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
        public class MstCategoryController : ControllerBase
    {
        private readonly ICategoriesRepository categoriesRepository;
                
        public MstCategoryController(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        [HttpGet]
     
        public  async Task<IActionResult> GetAllCategories()
        {
            List<CategoryDto> categories = await categoriesRepository.getAllCategories();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] AddCategoryDto Category)
        {
            var newCategory = await categoriesRepository.CreateAsyncCategory(Category);

            return Ok(newCategory);
        }
    }
}
