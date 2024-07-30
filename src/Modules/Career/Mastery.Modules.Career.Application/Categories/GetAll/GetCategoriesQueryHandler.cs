using System.Data;
using Dapper;
using Mastery.Modules.Career.Application.Abstractions.Data;
using Mastery.Modules.Career.Application.Abstractions.Messaging;
using Mastery.Modules.Career.Domain.Abstractions;

namespace Mastery.Modules.Career.Application.Categories.GetAll;

internal sealed class GetCategoriesQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetCategoriesQuery, CategoriesResponse>
{
    public async Task<Result<CategoriesResponse>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = sqlConnectionFactory.CreateConnection();

        string sql = """
            SELECT
                id AS Id
                value AS Value
                color_value AS Color
            FROM categories
            WHERE is_deleted = 'false'
            """;

        IEnumerable<CategoriesResponse.CategoryItem> categories = await connection.QueryAsync<CategoriesResponse.CategoryItem>(sql);

        return new CategoriesResponse([.. categories]);
    }
}
