namespace DocumentManagement.Data.Repo;
public class CategoryRepo(DMSDbContext _db) : ICategoryRepo
{
    public async Task<OneOf<CategoryOutputModelSimple, string>> CreateAsync(CategoryInputModel model)
    {
        if (await _db.Categories.AnyAsync(x => x.Name == model.Name))
            return "The is a category with the same name";
        var cat = model.MapTo<Category>();
        var tags = await _db.Tags.ToListAsync();
        cat.Tags = tags;
        _db.Categories.Add(cat);
        await _db.SaveChangesAsync();
        return cat.MapTo<CategoryOutputModelSimple>();
    }

    public async Task<string?> DeleteAsync(int[] Ids)
    {
        var cats = _db.Categories
            .Where(x => Ids.Contains(x.Id));

        if (await cats.AnyAsync(x => x.Documents.Count > 0))
            return "the category have Documents";

        await cats.ExecuteDeleteAsync();
        return null;
    }

    public async Task<PaginationModel<CategoryOutputModel>> FilterAsync(CategoryFilterModel filter)
    {
        return await _db.Categories.Where(x => (string.IsNullOrEmpty(filter.Name) || filter.Name == x.Name)
                                            && (filter.Tags.Length == 0 || x.Tags.Any(t => filter.Tags.Contains(t.Id))))
            .MapTo<CategoryOutputModel>()
            .ToPaginationModelAsync(filter);
    }

    public async Task<CategoryOutputModelDetailed> GetAsync(int id)
    {
        var res = await _db.Categories
            .Where(c => c.Id == id)
            .Include(c => c.Tags)
            .Select(x=>new CategoryOutputModelDetailed
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Tags = x.Tags.Select(t => t.Id).ToArray()
            })
            .FirstAsync();
        return res;
    }

    public async Task<List<CategoryOutputModelSimple>> GetAsync()
    {
        return await _db.Categories
            .Where(x=>x.Id != 1)
            .MapTo<CategoryOutputModelSimple>()
            .ToListAsync();
    }

    public async Task<OneOf<CategoryOutputModel, string>> UpdateAsync(CategoryUpdateModel model)
    {
        if (await _db.Categories.AnyAsync(x => x.Name == model.Name && x.Id != model.Id))
            return "the is a category with the same name";
        var cat = await _db.Categories.Where(x => x.Id == model.Id).Include(x => x.Tags).SingleAsync();
        cat.Name = model.Name;
        cat.Description = model.Description;
        cat.Tags = await _db.Tags.Where(x => model.Tags.Contains(x.Id)).ToListAsync();
        await _db.SaveChangesAsync();
        return cat.MapTo<CategoryOutputModel>();
    }
}
