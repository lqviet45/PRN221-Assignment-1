using BusinessObject;

namespace Repositories.abstraction;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetAllCategory();

    public Task<Category?> GetCategoryById(short id);

    public Task<bool> AddCategory(Category category);

    public Task<bool> UpdateCategory(Category category);

    public Task<bool> DeleteCategory(Category category);
}