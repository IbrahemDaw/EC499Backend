namespace DocumentManagement.Data.Repo;

public class TagRepo : ITagRepo
{
    private readonly DMSDbContext _db;
    public TagRepo(DMSDbContext context)
    {
        _db = context;
    }
    public async Task<TagOutputModel?> FindById(int id)
    {
        return await _db.Tags
            .Where(x => x.Id == id)
            .MapTo<TagOutputModel>()
            .SingleOrDefaultAsync();
    }
    public async Task<List<TagOutputModelSimple>> GetAll()
    {
        return await _db.Tags
            .MapTo<TagOutputModelSimple>()
            .ToListAsync();
    }

    public async Task<TagOutputModel> PostAsync(TagInputModel tag)
    {
        var test = await _db.Tags.Where(x => x.Name == tag.Name)
            .FirstOrDefaultAsync();
        if (test != null)
            return test.MapTo<TagOutputModel>();
        var res = tag.MapTo<Tag>();
        await _db.Tags.AddAsync(res);
        await _db.SaveChangesAsync();
        return res.MapTo<TagOutputModel>();
    }

    public async Task<OneOf<TagOutputModel, string>> UpdateAsync(TagUpdateModel value)
    {
        var tag = await _db.Tags.FindAsync(value.Id);
        if (tag == null)
            return "tag was not found";
        tag.Name = value.Name;
        tag.Description = value.Description;
        tag.IsEnable = value.IsEnable;
        await _db.SaveChangesAsync();
        return tag.MapTo<TagOutputModel>();
    }

    public async Task DeleteAsync(int[] ids)
    {
        var tags = await _db.Tags.Where(x => ids.Contains(x.Id))
           .ToListAsync();
        _db.Tags.RemoveRange(tags);
        await _db.SaveChangesAsync();
    }

    public async Task EnableAsync(int[] id)
    {
        var tags = await _db.Tags.Where(x => id.Contains(x.Id))
            .ToListAsync();

        tags.ForEach(x => x.IsEnable = true);

        await _db.SaveChangesAsync();
    }

    public async Task DisableAsync(int[] id)
    {
        var tags = await _db.Tags
            .Where(x => id.Contains(x.Id))
            .ToListAsync();

        foreach (var tag in tags)
        {
            tag.IsEnable = false;
        }
        await _db.SaveChangesAsync();
    }

    public async Task<PaginationModel<TagOutputModel>> FilterAsync(TagFilterModel filter)
    {
        return await _db.Tags
            .Where(x => (string.IsNullOrWhiteSpace(filter.Name) || x.Name.Contains(filter.Name)) &&
                      (string.IsNullOrEmpty(filter.Description) || x.Description.Contains(filter.Description)) &&
                      (filter.IsEnable == null || x.IsEnable == filter.IsEnable))
                      .MapTo<TagOutputModel>()
                      .ToPaginationModelAsync(filter);
    }
}