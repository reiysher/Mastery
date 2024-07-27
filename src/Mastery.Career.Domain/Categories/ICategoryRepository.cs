namespace Mastery.Career.Domain.Categories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(CategoryId categoryId, CancellationToken cancellationToken = default);

    void Add(Category category);
}
