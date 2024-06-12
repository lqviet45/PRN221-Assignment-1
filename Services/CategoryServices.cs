using BusinessObject;
using Repositories.abstraction;
using Services.abstraction;

namespace Services;

public class CategoryServices : ICategoryServices
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryServices(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAllCategory()
    {
        return await _categoryRepository.GetAllCategory().ConfigureAwait(false);
    }

    public async Task<Category?> GetCategoryById(short id)
    {
        return await _categoryRepository.GetCategoryById(id).ConfigureAwait(false);
    }

    public async Task<Category?> AddCategory(Category category)
    {
        var isSuccess = await _categoryRepository.AddCategory(category).ConfigureAwait(false);

        return isSuccess ? category : null;
    }

    public async Task<Category?> UpdateCategory(Category category)
    {
        var existCategory = await _categoryRepository.GetCategoryById(category.CategoryId).ConfigureAwait(false);

        if (existCategory is null)
        {
            throw new ArgumentNullException(nameof(category), "Category doesn't exist to update!!");
        }

        existCategory.CategoryName = category.CategoryName;
        existCategory.CategoryDesciption = category.CategoryDesciption;

        var isSuccess = await _categoryRepository.UpdateCategory(existCategory).ConfigureAwait(false);

        return isSuccess ? category : null;
    }

    public async Task<bool> DeleteCategory(short id)
    {
        var existCategory = await _categoryRepository.GetCategoryById(id).ConfigureAwait(false);

        if (existCategory is null)
        {
            return false;
        }
        
        if (existCategory.NewsArticles.Count > 0)
        {
            return false;
        }
        
        var isSuccess = await _categoryRepository.DeleteCategory(existCategory).ConfigureAwait(false);

        return isSuccess;
    }
}