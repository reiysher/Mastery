
namespace Mastery.Career.Domain;

public sealed class Company
{
    private Company() { }

    public Guid Id { get; init; }

    public Guid? RankId { get; private set; }

    public static Company New(Guid id)
    {
        return new Company()
        {
            Id = id,
            RankId = null,
        };
    }

    public void SetRank(Guid rankId)
    {
        RankId = rankId;
    }
}
