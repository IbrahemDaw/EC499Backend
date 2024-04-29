// Ignore Spelling: Validator
namespace DMS.Data.Models.Folders;
public class FolderInputModel
{
    public string Name { get; set; } = null!;
    public int MainFolderId { get; set; } 
}
public class FolderInputValidator : AbstractValidator<FolderInputModel>
{
    public FolderInputValidator()
    {
        RuleFor(x => x.MainFolderId)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .Matches("^[a-zA-Z]+$")
            .WithMessage("Name should only contain letters.");
    }
}
public class FolderUpdateModel
{
    public int Id { get; set; }
    public string? Name { get; set; } = null!;
    public int? MainFolderId { get; set; }

}
public class FolderUpdateModelValidator : AbstractValidator<FolderUpdateModel>
{
    public FolderUpdateModelValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .Matches("^[a-zA-Z]+$")
            .When(x => string.IsNullOrWhiteSpace(x.Name));
    }
}
public class FolderOutputModelSimple
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public int? MainFolderId { get; set; }

}
public class FolderOutputModel : FolderOutputModelSimple
{
    public ICollection<DocumentOutputModelSimple> Documents { get; set; } = [];
    public ICollection<FolderOutputModelSimple> SubFolders { get; set; } = [];
    public FolderOutputModelSimple? MainFolder { get; set; }
}
