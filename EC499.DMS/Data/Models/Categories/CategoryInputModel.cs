namespace DMS.Data.Models.Categories;

public class CategoryInputModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int[] Tags { get; set; } = [];
}
public class CategoryInputModelValidator : AbstractValidator<CategoryInputModel>
{
    public CategoryInputModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(256);
    }
}