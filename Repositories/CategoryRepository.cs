using BusinessObject;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.abstraction;

namespace Repositories;

public class CategoryRepository : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetAllCategory()
        => await CategoryDao.GetAll().ToListAsync();

    public async Task<Category?> GetCategoryById(short id)
        => await CategoryDao.GetByIdAsync(id);

    public async Task<bool> AddCategory(Category category)
        => await CategoryDao.AddAsync(category);

    public async Task<bool> UpdateCategory(Category category)
        => await CategoryDao.UpdateAsync(category);

    public async Task<bool> DeleteCategory(Category category)
        => await CategoryDao.DeleteAsync(category);
}