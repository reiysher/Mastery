namespace Mastery.Modules.Career.Domain.Categories;

public static class CategoryErrors
{
    public static readonly Error NotFound = new("Category.NotFound", "Category with specified identifier not found");

    public static readonly Error InvalidValue = new("Category.Value", "Provided value is invalid");

    public static readonly Error InvalidColor = new("Category.Color", "Provided color is invalid");

    public static readonly Error InvalidDescription = new("Category.Description", "Provided description is invalid");
}
