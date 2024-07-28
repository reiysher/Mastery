using Dapper;
using Mastery.Career.Application.Abstractions.Data;
using Mastery.Career.Application.Abstractions.Messaging;
using Mastery.Career.Domain.Abstractions;

namespace Mastery.Career.Application.Categories.GetAll;

internal sealed class GetCategoriesQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetCategoriesQuery, CategoriesResponse>
{
    public async Task<Result<CategoriesResponse>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        using var connection = sqlConnectionFactory.CreateConnection();

        string sql = """
            SELECT
                id AS Id
                value AS Value
                color_value AS Color
            FROM categories
            WHERE is_deleted = 'false'
            """;

        var categories = await connection.QueryAsync<CategoriesResponse.CategoryItem>(sql);

        return new CategoriesResponse([..categories]);
    }
}
