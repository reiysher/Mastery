using Mastery.Career.Domain.Categories;

namespace Mastery.Career.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository(ApplicationDbContext dbContext)
    : Repository<Category, Guid>(dbContext), ICategoryRepository;
