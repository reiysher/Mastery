using System.Data;
using Dapper;
using Mastery.Common.Application.Data;
using Mastery.Common.Application.Messaging;
using Mastery.Common.Domain;

namespace Mastery.Modules.Career.Application.Categories.GetAll;

internal sealed class GetCategoriesQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory)
    : IQueryHandler<GetCategoriesQuery, CategoriesResponse>
{
    public async Task<Result<CategoriesResponse>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        using IDbConnection connection = await sqlConnectionFactory.OpenConnectionAsync(cancellationToken);

        const string sql =
            $"""
            SELECT
                id AS {nameof(CategoriesResponse.CategoryItem.Id)}
                value AS {nameof(CategoriesResponse.CategoryItem.Value)}
                color_value AS {nameof(CategoriesResponse.CategoryItem.Color)}
            FROM categories
            WHERE is_deleted = 'false'
            """;

        IEnumerable<CategoriesResponse.CategoryItem> categories = await connection.QueryAsync<CategoriesResponse.CategoryItem>(sql);

        return new CategoriesResponse([.. categories]);
    }
}
