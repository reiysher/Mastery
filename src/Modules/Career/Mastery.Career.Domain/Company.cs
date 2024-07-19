
namespace Mastery.Career.Domain;

public sealed class Company
{
    private Company() { }

    public required CompanyId Id { get; init; }

    public RankId? RankId { get; private set; }

    public static Company New(CompanyId id)
    {
        return new Company()
        {
            Id = id,
            RankId = null,
        };
    }

    public void SetRank(RankId rankId)
    {
        RankId = rankId;
    }
}
