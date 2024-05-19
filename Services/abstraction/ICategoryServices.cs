using BusinessObject;

namespace Services.abstraction;

public interface ICategoryServices
{
    public Task<IEnumerable<Category>> GetAllCategory();

    public Task<Category?> GetCategoryById(short id);

    public Task<Category?> AddCategory(Category category);

    public Task<Category?> UpdateCategory(Category category);

    public Task<bool> DeleteCategory(short category);
}