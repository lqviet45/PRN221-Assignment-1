using BusinessObject;

namespace Services.abstraction;

public interface ITagServices
{
    public Task<IEnumerable<Tag>> GetAllTag();
    public Task<Tag?> AddTag(Tag? tag);
}