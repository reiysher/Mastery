using Mastery.Modules.Career.Domain.Categories;

namespace Mastery.Modules.Career.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository(ApplicationDbContext dbContext)
    : Repository<Category, Guid>(dbContext), ICategoryRepository;
