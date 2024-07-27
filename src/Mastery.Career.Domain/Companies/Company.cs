namespace Mastery.Career.Domain.Companies;

public sealed class Company : Aggregate<CompanyId>
{
    private Company() { }

    public CompanyTitle Title { get; private set; } = default!;

    public CompanyCategory? Category { get; private set; }

    private readonly List<Rating> _ratings = [];

    public IReadOnlyCollection<Rating> Ratings => _ratings.AsReadOnly();

    public Note? Note { get; private set; }

    public static Company Create(Guid id, string title)
    {
        return new Company
        {
            Id = CompanyId.From(id),
            Title = CompanyTitle.From(title),
            Category = null,
            Note = null,
        };
    }
}
