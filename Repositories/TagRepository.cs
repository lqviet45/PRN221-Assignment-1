using BusinessObject;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repositories.abstraction;

namespace Repositories;

public class TagRepository : ITagRepository
{
    public async Task<IEnumerable<Tag>> GetAll()
        => await TagDao.GetAll().ToListAsync();

    public async Task<bool> AddTag(Tag tag)
        => await TagDao.AddAsync(tag).ConfigureAwait(false);

    public async Task<bool> UpdateCategory(Tag tag)
        => await TagDao.UpdateAsync(tag).ConfigureAwait(false);

    public async Task<bool> DeleteCategory(Tag tag)
        => await TagDao.DeleteAsync(tag).ConfigureAwait(false);
}