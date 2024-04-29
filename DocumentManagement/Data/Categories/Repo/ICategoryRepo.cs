namespace DocumentManagement.Data.Repo;
public interface ICategoryRepo
{
    Task<CategoryOutputModelDetailed> GetAsync(int id);
    Task<List<CategoryOutputModelSimple>> GetAsync();
    Task<OneOf<CategoryOutputModelSimple, string>> CreateAsync(CategoryInputModel model);
    Task<string?> DeleteAsync(int[] Ids);
    Task<PaginationModel<CategoryOutputModel>> FilterAsync(CategoryFilterModel filter);
    Task<OneOf<CategoryOutputModel, string>> UpdateAsync(CategoryUpdateModel model);
}
