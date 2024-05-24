using BusinessObject;

namespace Repositories.abstraction;

public interface ITagRepository
{
    public Task<IEnumerable<Tag>> GetAll();
    public Task<bool> AddTag(Tag tag);

    public Task<bool> UpdateCategory(Tag tag);

    public Task<bool> DeleteCategory(Tag tag);
}