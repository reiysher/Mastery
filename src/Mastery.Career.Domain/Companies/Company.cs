﻿using Mastery.Career.Domain.Categories;

namespace Mastery.Career.Domain.Companies;

public sealed class Company : Aggregate<Guid>
{
    public CompanyTitle Title { get; private set; } = default!;

    public CompanyCategory? Category { get; private set; }

    private readonly List<Rating> _ratings = [];

    public IReadOnlyCollection<Rating> Ratings => _ratings.AsReadOnly();

    public Note? Note { get; private set; }

    private Company() { }

    public static Company Create(Guid id, string? title, string? note = "", Category? category = null)
    {
        var company = new Company
        {
            Id = id,
            Title = CompanyTitle.From(title),
            Category = CompanyCategory.From(category),
            Note = Note.New(note),
        };

        company.RaiseDomainEvent(new CompanyCreatedDomainEvent(company.Id));

        return company;
    }

    public void ChangeTitle(string? title)
    {
        if (!string.IsNullOrWhiteSpace(title) && title != Title.Value)
        {
            Title = CompanyTitle.From(title);

            RaiseDomainEvent(new CompanyTitleChangedDomainEvent(Id, Title.Value));
        }
    }

    public void ChangeCategory(Category? category)
    {
        ArgumentNullException.ThrowIfNull(category, nameof(category));

        if (category.Id != Category?.Id)
        {
            Category = CompanyCategory.From(category);

            RaiseDomainEvent(new CompanyCategoryChangedDomainEvent(Id, Category?.Id));
        }
    }

    public void ResetCategory()
    {
        Category = CompanyCategory.Default();

        RaiseDomainEvent(new CompanyCategoryChangedDomainEvent(Id, Category?.Id));
    }

    public void WriteNote(string? value)
    {
        Note = Note.New(value);

        RaiseDomainEvent(new CompanyNoteWrittenDomainEvent(Id, Note.Value));
    }
}
