namespace DocumentManagement.Data.Repo;

public interface ITagRepo
{
    Task<TagOutputModel?> FindById(int id);
    Task<List<TagOutputModelSimple>> GetAll();
    Task<TagOutputModel> PostAsync(TagInputModel tag);
    Task<OneOf<TagOutputModel, string>> UpdateAsync(TagUpdateModel value);
    Task DeleteAsync(int[] ids);
    Task EnableAsync(int[] id);
    Task DisableAsync(int[] id);
    Task<PaginationModel<TagOutputModel>> FilterAsync(TagFilterModel filter);
}
