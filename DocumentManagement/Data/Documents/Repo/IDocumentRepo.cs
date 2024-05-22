namespace DocumentManagement.Data.Repo;
public interface IDocumentRepo
{
    Task<DocumentOutputModel?> GetByIdAsync(int Id);
    Task<PaginationModel<DocumentOutputModelSimple>> FilterAsync(DocumentFilterModel filter);
    Task UpdateAsync(DocumentUpdateModel model);
    Task DeleteAsync(int[] Ids);
    Task UplaodAsync(DocumentInputModel model);
    Task<DocumentDownloadModel> DownloadAsync(int Id);
    Task<PaginationModel<DocumentOutputModelSimple>> GraduationProjectFilterAsync(DocumentFilterModel filter);
}