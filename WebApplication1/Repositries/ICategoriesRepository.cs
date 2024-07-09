using WebApplication1.Models.Domain;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Repositries
{
    public interface ICategoriesRepository
    {
        Task<List<CategoryDto>> getAllCategories();
        Task<CategoryDto> CreateAsyncCategory(AddCategoryDto category);
    }
}
