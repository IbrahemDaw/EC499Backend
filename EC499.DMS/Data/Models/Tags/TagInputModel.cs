// Ignore Spelling: Validator

using FluentValidation;

namespace DMS.Data.Models.Tags;

public class TagInputModel
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsEnable { get; set; } = true;
}
public class TagInputValidator : AbstractValidator<TagInputModel>
{
    public TagInputValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(256)
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}
public class TagUpdateModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } ="";
    public bool IsEnable { get; set; }
}
public class TagUpdateValidator : AbstractValidator<TagUpdateModel>
{
    public TagUpdateValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(256);
    }
}