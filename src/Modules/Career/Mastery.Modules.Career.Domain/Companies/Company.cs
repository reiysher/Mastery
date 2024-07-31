using Mastery.Modules.Career.Domain.Categories;
using Mastery.Modules.Career.Domain.SharedKernel;

namespace Mastery.Modules.Career.Domain.Companies;

public sealed class Company : Aggregate<Guid>
{
    public CompanyTitle Title { get; private set; } = default!;

    public CompanyCategory? Category { get; private set; }

    private readonly List<Rating> _ratings = [];

    public IReadOnlyCollection<Rating> Ratings => _ratings.AsReadOnly();

    public Note? Note { get; private set; }

    private Company() { }

    public static Result<Company> Create(Guid id, string? title, string? note = "", Category? category = null)
    {
        Result<CompanyTitle> companyTitleResult = CompanyTitle.From(title);
        if (companyTitleResult.IsFailure)
        {
            return Result.Failure<Company>(companyTitleResult.Error);
        }

        Result<Note> noteResult = Note.New(note);
        if (noteResult.IsFailure)
        {
            return Result.Failure<Company>(noteResult.Error);
        }

        var company = new Company
        {
            Id = id,
            Title = companyTitleResult.Value,
            Category = CompanyCategory.From(category),
            Note = noteResult.Value,
        };

        company.Raise(new CompanyCreatedDomainEvent(company.Id));

        return company;
    }

    public Result ChangeTitle(string? title)
    {
        if (!string.IsNullOrWhiteSpace(title) && title != Title.Value)
        {
            Result<CompanyTitle> companyTitleResult = CompanyTitle.From(title);
            if (companyTitleResult.IsFailure)
            {
                return Result.Failure<Company>(companyTitleResult.Error);
            }

            Raise(new CompanyTitleChangedDomainEvent(Id, Title.Value));
        }

        return Result.Success();
    }

    public Result ChangeCategory(Category? category)
    {
        if (category is null)
        {
            return Result.Failure(CompanyErrors.InvalidCategory);
        }

        if (category.Id != Category?.Id)
        {
            Category = CompanyCategory.From(category);

            Raise(new CompanyCategoryChangedDomainEvent(Id, Category?.Id));
        }

        return Result.Success();
    }

    public void ResetCategory()
    {
        Category = CompanyCategory.Default();

        Raise(new CompanyCategoryChangedDomainEvent(Id, Category?.Id));
    }
    public Result WriteNote(string? value)
    {

        Result<Note> noteResult = Note.New(value);
        if (noteResult.IsFailure)
        {
            return Result.Failure(noteResult.Error);
        }

        Note = noteResult.Value;

        Raise(new CompanyNoteWrittenDomainEvent(Id, Note.Value));

        return Result.Success();
    }
}
