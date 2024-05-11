namespace Infrastructure.DataSeed;

public static class CategorySeed
{
    public static List<Category> GetCategorySeed()
    {
        return new List<Category>{
            new Category{
                Id = 1,
                Name = "Graduation Projects",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
         };
    }
}
