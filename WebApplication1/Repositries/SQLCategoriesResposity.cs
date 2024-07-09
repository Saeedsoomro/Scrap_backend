using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Repositries
{
    public class SQLCategoriesResposity : ICategoriesRepository
    {
        private readonly ScrapMangementDbContext dbContext;

        public SQLCategoriesResposity(ScrapMangementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        async Task<CategoryDto> ICategoriesRepository.CreateAsyncCategory(AddCategoryDto category)
        {
            var newCategory = new Categories()
            {
                Name = category.Name,
                Title = category.Title,
            };

            await dbContext.Categories.AddAsync(newCategory);
            await dbContext.SaveChangesAsync();

            // Map Categories entity to CategoryDto
            var categoryDto = new CategoryDto
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                Title = newCategory.Title,
                // Map other properties if needed
            };

            return categoryDto;

        }

        async Task<List<CategoryDto>>  ICategoriesRepository.getAllCategories()
        {
            List<Categories> categories =  await dbContext.Categories.ToListAsync();

             var categoriesDto = new List<CategoryDto>();
            
            foreach (var category in categories)
            {
                categoriesDto.Add(new CategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Title = category.Title,
                });
            }

            return categoriesDto;
          
        }
    }
}
