using Mastery.Common.Infrastructure.Repositories;
using Mastery.Modules.Career.Domain.Categories;

namespace Mastery.Modules.Career.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository(CareerDbContext dbContext)
    : Repository<Category, Guid>(dbContext), ICategoryRepository;
