using BusinessObject;
using Microsoft.Extensions.Logging.Abstractions;
using Repositories.abstraction;
using Services.abstraction;

namespace Services;

public class TagServices : ITagServices
{
    private readonly ITagRepository _tagRepository;

    public TagServices(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<IEnumerable<Tag>> GetAllTag()
    {
        return await _tagRepository.GetAll();
    }

    public async Task<Tag?> AddTag(Tag? tag)
    {
        var isSuccess = await _tagRepository.AddTag(tag);
        
        return isSuccess ? tag : null;
    }
}